using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface IUserService
    {
        Task<LazyLoadedResult<Profile>> GetAllUsersAsync(UserIdentityParams user, UserFilteringParams filter = null, UserSortingParameters sorting = null);
        Task<Profile> GetUserByIdAsync(string id, UserIdentityParams user);
        Task UpdateUserByIdAsync(string id, BaseProfile model);
        Task<Profile> CreateUserAsync(ProfileCreatable model);
        Task DeleteUserByIdAsync(string id);
        Task SetUsersCache();
    }
}
