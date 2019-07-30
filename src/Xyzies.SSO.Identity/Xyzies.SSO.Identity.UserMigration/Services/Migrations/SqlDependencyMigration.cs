using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using Xyzies.SSO.Identity.CPUserMigration.Models;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.UserMigration.Services.Migrations;

namespace Xyzies.SSO.Identity.CPUserMigration.Services.Migrations
{
    public class SqlDependencyMigration : ISqlDependencyMigration
    {
        private SqlTableDependency<User> _dependency;
        private readonly IMigrationService _migrationService;
        private readonly ILogger _logger;

        private string _connectionString;
        private string _tableName = "TWC_Users";

        public SqlDependencyMigration(IMigrationService migrationService, ILogger logger, IOptionsMonitor<MigrationSettings> options)
        {
            _migrationService = migrationService;
            _connectionString = options.CurrentValue?.CpDb;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Initialize()
        {
            try
            {
                _dependency = new SqlTableDependency<User>(_connectionString, _tableName);
                _dependency.OnChanged += OnChange;
                _dependency.Start();
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Subscribe failed - {ex.Message}");
            }
        }

        public async void OnChange(object sender, RecordChangedEventArgs<User> e)
        {
            await _migrationService.MigrateByTrigger(e.ChangeType, e.Entity);
        }
    }
}
