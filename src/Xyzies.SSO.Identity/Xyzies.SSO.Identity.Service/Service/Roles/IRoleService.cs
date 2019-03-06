using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models.Permissions;

namespace Xyzies.SSO.Identity.Services.Service.Roles
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetAllAsync();
    }
}
