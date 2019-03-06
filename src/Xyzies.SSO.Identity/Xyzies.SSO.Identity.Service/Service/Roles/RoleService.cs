using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Models.Permissions;

namespace Xyzies.SSO.Identity.Services.Service.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository = null;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<List<RoleModel>> GetAllAsync()
        {
            var rolesDb = (await _roleRepository.GetAsync()).ToList();
            return rolesDb.Adapt<List<RoleModel>>();
        }
    }
}
