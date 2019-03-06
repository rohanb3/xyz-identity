using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class RoleRepository : EfCoreBaseRepository<Guid, Role>, IRepository<Guid, Role>, IRoleRepository
    {
        public RoleRepository(DbContext dbContext)
            : base(dbContext)
        {

        }

        public override async Task<IQueryable<Role>> GetAsync() =>
            await Task.FromResult(base.Data
                .Include(r => r.RelationToPolicy)
                    .ThenInclude(rp => rp.Entity1)
                    .ThenInclude(p => p.RelationToPermission)
                    .ThenInclude(pp => pp.Entity1)
                .AsQueryable());
    }
}
