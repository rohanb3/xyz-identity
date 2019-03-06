using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface IUserService
    {
        Task<IEnumerable<Profile>> GetAllUsersAsync(UserFilteringParams filter = null);
        Task<Profile> GetUserByIdAsync(string id);
        Task UpdateUserByIdAsync(string id, BaseProfile model);
        Task<Profile> CreateUserAsync(ProfileCreatable model);
        Task DeleteUserByIdAsync(string id);
    }
}
