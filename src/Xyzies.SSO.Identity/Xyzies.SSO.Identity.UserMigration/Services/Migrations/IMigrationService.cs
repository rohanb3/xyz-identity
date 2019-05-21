using System.Threading.Tasks;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services.Migrations
{
    public interface IMigrationService
    {
        Task PeriodicTask();
        Task ReplaceRoleIdWithRoleName();
        Task MigrateCPToAzureAsync(MigrationOptions options);
        Task SyncEnabledUsers(MigrationOptions options);
        Task FillNullRolesWithAnonymous();
        Task SetAllEmailsToLowerCase(MigrationOptions options);
        Task MigrateAzureToCPAsync();
    }
}