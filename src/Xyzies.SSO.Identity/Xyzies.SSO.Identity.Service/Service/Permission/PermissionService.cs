using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PermissionService> _logger = null;

        public PermissionService(IMemoryCache memoryCache, 
            IRoleService roleService,
            ILogger<PermissionService> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool CheckPermission(string role, string[] scopes)
        {
            var roles = _memoryCache.Get<List<RoleModel>>(Consts.Cache.PermissionKey);
            var roleModel = roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower());
            if (roleModel != null)
            {
                _logger.LogInformation($"RoleName{roleModel.RoleName}");
                foreach (var scope in scopes)
                {
                    if (roleModel.Policies.FirstOrDefault(policy => policy.Scopes.FirstOrDefault(s => s.ScopeName == scope) != null) == null)
                    {
                        _logger.LogInformation($"Not fpund scope by policy");
                        return false;
                    };
                }
                _logger.LogInformation($"Has permission");
                return true;
            }
            _logger.LogInformation($"Role is null");
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

        public async Task<IEnumerable<string>> GetScopesByRole(string role)
        {
            await CheckPermissionExpiration();
            var roles = _memoryCache.Get<List<RoleModel>>(Consts.Cache.PermissionKey);
            var roleModel = roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower());

            return roleModel?.Policies.SelectMany(policy => policy.Scopes.Select(scope => scope.ScopeName)) ?? throw new ArgumentException("Unknown role", "Role");
        }

        private async Task SetPermissionObject()
        {
            var permission = await _roleService.GetAllAsync();

            _memoryCache.Set(Consts.Cache.PermissionKey, permission);
            _memoryCache.Set(Consts.Cache.ExpirationKey, DateTime.Now.AddHours(1));
        }
    }
}
