using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IUserService
    {
        Task<LazyLoadedResult<Profile>> GetAllUsersAsync(UserIdentityParams user, UserFilteringParams filter = null, UserSortingParameters sorting = null);
        Task<Profile> GetUserByIdAsync(string id, UserIdentityParams user);
        Task<Profile> GetUserBy(Func<AzureUser, bool> predicate);
        Dictionary<string, int> GetUsersCountInCompanies(List<string> companyIds = null, UserSortingParameters sorting = null, LazyLoadParameters lazyParameters = null);
        Task UpdateUserByIdAsync(string id, BaseProfile model);
        Task<Profile> CreateUserAsync(ProfileCreatable model, string token);
        Task DeleteUserByIdAsync(string id);
        Task SetUsersCache();
        Task UploadAvatar(string userId, AvatarModel avatarModel);
        Task DeleteAvatar(string userId);
        Task<FileModel> GetAvatar(string userId);
        Task UpdateUserPasswordAsync(string userMail, string password);
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
