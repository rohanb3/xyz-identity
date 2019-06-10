using System;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.CPUserMigration.Models;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services.Migrations
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
        /// <summary>
        /// Change role from operator to support admin
        /// </summary>
        /// <returns></returns>
        Task ChangeRoleName();

        /// <summary>
        /// Gets last sync time for all users
        /// </summary>
        /// <returns>Last sync time for full users background sync</returns>
        Task<LastSyncTime> GetLastUsersFullSyncTime();
    }
}