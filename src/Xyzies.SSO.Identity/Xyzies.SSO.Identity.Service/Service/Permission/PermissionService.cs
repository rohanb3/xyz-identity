using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Services.Models.Permissions;
using Xyzies.SSO.Identity.Services.Service.Roles;

namespace Xyzies.SSO.Identity.Services.Service.Permission
{
    public class PermissionService : IPermissionService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRoleService _roleService;

        public PermissionService(IMemoryCache memoryCache, IRoleService roleService)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        public bool CheckPermission(string role, string[] scopes)
        {
            var roles = _memoryCache.Get<List<RoleModel>>(Consts.Cache.PermissionKey);
            var roleModel = roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower());

            if (roleModel != null)
            {
                foreach (var scope in scopes)
                {
                    if (roleModel.Policies.FirstOrDefault(policy => policy.Scopes.FirstOrDefault(s => s.ScopeName == scope) != null) == null)
                    {
                        return false;
                    };
                }
                return true;
            }
            return false;
        }

        public async Task CheckPermissionExpiration()
        {
            var cacheExpiration = _memoryCache.Get<DateTime>(Consts.Cache.ExpirationKey);
            var permissions = _memoryCache.Get<List<RoleModel>>(Consts.Cache.PermissionKey);
            if (cacheExpiration < DateTime.Now || permissions?.Count == 0)
            {
                await SetPermissionObject();
            }
        }

        private async Task SetPermissionObject()
        {
            var permission = await _roleService.GetAllAsync();

            _memoryCache.Set(Consts.Cache.PermissionKey, permission);
            _memoryCache.Set(Consts.Cache.ExpirationKey, DateTime.Now.AddHours(1));
        }
    }
}
