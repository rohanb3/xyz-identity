using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.Services.Service.Permission
{
    public interface IPermissionService
    {
        bool CheckPermission(string role, string[] scopes);
        Task CheckPermissionExpiration();
    }
}
