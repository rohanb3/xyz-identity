using System.Threading.Tasks;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services
{
    public interface IMigrationService
    {
        Task MigrateAsync(MigrationOptions options);
        Task SyncEnabledUsers(MigrationOptions options);

        Task ResetCPRolesForExistingUsersAsync(MigrationOptions options);

    }
}
