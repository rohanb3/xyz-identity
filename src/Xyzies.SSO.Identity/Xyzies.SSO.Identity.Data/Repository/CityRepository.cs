using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class CityRepository : EfCoreBaseRepository<Guid, City>, IRepository<Guid, City>, ICityRepository
    {
        public CityRepository(IdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
