
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Services.Service.Permission
{
    public interface IPermissionService
    {
        bool CheckPermission(string role, string[] scopes);
        Task CheckPermissionExpiration();

        Task<IEnumerable<string>> GetScopesByRole(string role);
    }
}
