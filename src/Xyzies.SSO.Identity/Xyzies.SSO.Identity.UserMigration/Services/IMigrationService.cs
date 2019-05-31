using System.Threading.Tasks;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services
{
    public interface IMigrationService
    {
        Task FillSuperAdminsWithDefaultBranches(string token, MigrationOptions options = null);
        Task ReplaceRoleIdWithRoleName();
        Task MigrateCPToAzureAsync(MigrationOptions options, string token);
        Task UpdateUserActivityStatus(MigrationOptions options = null);
        Task FillNullRolesWithAnonymous();
        Task SetAllEmailsToLowerCase(MigrationOptions options);
        Task MigrateAzureToCPAsync();
        /// <summary>
        /// Change role from operator to support admin
        /// </summary>
        /// <returns></returns>
        Task ChangeRoleName();
    }
}
