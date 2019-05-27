using System.Threading.Tasks;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services
{
    public interface IMigrationService
    {
        Task FillSuperAdminsWithDefaultBranches(string token, MigrationOptions options = null);
        Task ReplaceRoleIdWithRoleName();
        Task MigrateCPToAzureAsync(MigrationOptions options);
        Task UpdateUserActivityStatus(MigrationOptions options = null);
        Task FillNullRolesWithAnonymous();
        Task SetAllEmailsToLowerCase(MigrationOptions options);
        Task MigrateAzureToCPAsync();
    }
}
