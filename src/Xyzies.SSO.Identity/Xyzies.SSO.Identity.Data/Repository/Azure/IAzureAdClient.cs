using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity.Azure;

namespace Xyzies.SSO.Identity.Data.Repository.Azure
{
    public interface IAzureAdClient
    {
        Task<AzureUser> GetUserById(string id);
        Task<AzureUsersResponse> GetUsers(string filter = "", int userCount = 100, bool takeFromDirectoryObjects = false);
        Task<AzureUser> PostUser(AzureUser user);
        Task PatchUser(string id, AzureUser user);
        Task DeleteUser(string id);
        Task PutAvatar(string userId, byte[] avatarFile);
        Task<FileModel> GetAvatar(string userId);
    }
}
