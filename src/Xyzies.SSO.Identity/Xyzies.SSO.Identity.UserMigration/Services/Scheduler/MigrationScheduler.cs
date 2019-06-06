﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.UserMigration.Models;
using Xyzies.SSO.Identity.UserMigration.Services.Migrations;

namespace Xyzies.SSO.Identity.CPUserMigration.Services.Scheduler
{
    public class MigrationScheduler : IMigrationScheduler

    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private CancellationToken _token;
        private Timer _timer;

        private const int _fetchPeriodSecond = 3600; 
        private const int _usersLimit = 500;

        public MigrationScheduler(ILogger<MigrationScheduler> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background User Migration is starting.");

            _token = cancellationToken;
            _timer = new Timer(MigrateUsers, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(_fetchPeriodSecond));

            return Task.CompletedTask;
        }

        private async void MigrateUsers(object state)
        {
            try
            {

                _logger.LogInformation("Timed Background Users Migration begin");
                using (var serviceScope = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var migrationService = serviceScope.ServiceProvider.GetService<IMigrationService>();
                    var cpUserRepository = serviceScope.ServiceProvider.GetService<ICpUsersRepository>();

                    var users = await cpUserRepository.GetAsync();

                    var tasks = new List<Task>();
                    var offset = 0;
                    var totalUsers = users.Count();

                    _logger.LogInformation($"Total users cout from CP: {totalUsers}");

                    do
                    {
                        tasks.Add(migrationService.MigrateCPToAzureAsync(new MigrationOptions() { Limit = _usersLimit, Offset = offset }));
                        _logger.LogInformation($"Fetch started with params: limit - {_usersLimit}, offset - {offset}");

                        offset += _usersLimit;
                    } while (offset < totalUsers);

                    Task.WaitAll(tasks.ToArray());

                    _logger.LogInformation("All fetches ended");

                    if (_token.IsCancellationRequested)
                    {
                        _logger.LogInformation("Canceliation was requested");
                        _timer?.Change(Timeout.Infinite, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Unexpected error in background user sync, {ex}", ex);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background User Migration is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}