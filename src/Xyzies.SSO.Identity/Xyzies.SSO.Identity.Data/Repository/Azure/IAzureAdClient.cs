using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity.Azure;

namespace Xyzies.SSO.Identity.Data.Repository.Azure
{
    public interface IAzureAdClient
    {
        Task<AzureUser> GetUserById(string id);
        Task<IEnumerable<AzureUser>> GetUsers(string filter = null);
        Task PostUser(AzureUser user);
        Task PatchUser(string id, AzureUser user);
        Task DeleteUser(string id);
    }
}
