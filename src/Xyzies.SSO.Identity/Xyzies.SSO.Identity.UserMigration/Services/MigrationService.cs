using Mapster;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services
{
    public class MigrationService : IMigrationService
    {
        private readonly ICpUsersRepository _cpUsersRepository;
        private readonly IAzureAdClient _azureClient;
        private readonly IRoleRepository _roleRepository;


        public MigrationService(ICpUsersRepository cpUsersRepository, IAzureAdClient azureClient, IRoleRepository roleRepository)
        {
            _cpUsersRepository = cpUsersRepository ?? throw new ArgumentNullException(nameof(cpUsersRepository));
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public async Task MigrateAsync(MigrationOptions options)
        {
            try
            {
                var users = await _cpUsersRepository.GetAsync(x => !(x.IsDeleted ?? false));
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());

                var roles = (await _roleRepository.GetAsync()).ToList();

                foreach (var user in users)
                {
                    user.Role = roles.FirstOrDefault(role => role.RoleId == user.RoleId)?.RoleName;
                    await _azureClient.PostUser(user.Adapt<AzureUser>());
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task UpdateBranchId(UserFilteringParams options)
        {
            try
            {
                var users = await _azureClient.GetUsers(FilterConditions.GetUserFilterString(options));

                foreach (var user in users)
                {
                    if ((user.Role == "RetailerAdmin" || user.Role == "SalesRep"))
                    {
                        if (int.TryParse(user.BranchId, out int BranchId))
                        {
                            user.BranchId = "ff9f2e2f-e4cc-4c10-a279-12d68cdb45ce";
                            await _azureClient.PatchUser(user.ObjectId, user);
                        }
                    }
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
        }
    }

}
