using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IAzureAdClient _azureClient;

        public UserService(IAzureAdClient azureClient)
        {
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
        }

        public async Task<IEnumerable<Profile>> GetAllUsersAsync(UserFilteringParams filter = null)
        {
            var users = await _azureClient.GetUsers(FilterConditions.GetUserFilterString(filter));

            return users.Adapt<List<Profile>>();
        }

        public async Task UpdateUserByIdAsync(string id, BaseProfile model)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                await _azureClient.PatchUser(id, model.Adapt<AzureUser>());
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<Profile> CreateUserAsync(ProfileCreatable model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                await _azureClient.PostUser(model.Adapt<AzureUser>());
                return model.Adapt<Profile>();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<Profile> GetUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Object id can not be null or empty");
            }

            try
            {
                var user = await _azureClient.GetUserById(id);
                return user.Adapt<Profile>();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

        public async Task DeleteUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Object id can not be null or empty");
            }

            try
            {
                await _azureClient.DeleteUser(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (AccessViolationException)
            {
                throw;
            }
        }
    }
}
