using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class CpUsersRepository : EfCoreBaseRepository<int, User>, IRepository<int, User>, ICpUsersRepository
    {
        public CpUsersRepository(CablePortalIdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
