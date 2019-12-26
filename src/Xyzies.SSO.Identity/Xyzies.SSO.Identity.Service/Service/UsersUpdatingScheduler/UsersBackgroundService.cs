using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Xyzies.SSO.Identity.Services.Models;
using Xyzies.SSO.Identity.Services.Service;

namespace Xyzies.SSO.Identity.Service.Service.UsersUpdatingScheduler
{
    public class UsersBackgroundService : IUsersBackgroundService, IHostedService
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        private int _updatePeriod;
        private Timer _timer;

        public UsersBackgroundService(IServiceProvider serviceProvider, IOptionsMonitor<ProjectSettingsOption> options,
            ILogger<UsersBackgroundService> logger
        )
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider;
            var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _userService = serviceScope.ServiceProvider.GetService<IUserService>();
            int.TryParse(options?.CurrentValue?.UsersResetTime, out _updatePeriod);
            if (_updatePeriod == 0)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(UpdateUsersTimer,
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(_updatePeriod));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task UpdateUsers()
        {
            try
            {
                await _userService.SetUsersCache();
            }
            catch
            {
                _logger.LogCritical("[BG] Users Cache Fetch Failed!! \n Something wrong with _userService.SetUsersCache method!");
            }
        }

        public async void UpdateUsersTimer(object state)
        {
            await UpdateUsers();
        }
    }
}
