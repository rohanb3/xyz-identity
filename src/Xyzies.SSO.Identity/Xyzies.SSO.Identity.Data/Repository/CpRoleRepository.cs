using System;
using System.Collections.Generic;
using System.Text;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class CpRoleRepository : EfCoreBaseRepository<Guid, CPRole>, IRepository<Guid, CPRole>, ICpRoleRepository
    {
        public CpRoleRepository(CablePortalIdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
