﻿using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class CpRoleRepository : EfCoreBaseRepository<Guid, CpRoles>, IRepository<Guid, CpRoles>, ICpRoleRepository
    {
        public CpRoleRepository(CablePortalIdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
