using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class StateRepository : EfCoreBaseRepository<Guid, State>, IRepository<Guid, State>, IStateRepository
    {
        public StateRepository(IdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
