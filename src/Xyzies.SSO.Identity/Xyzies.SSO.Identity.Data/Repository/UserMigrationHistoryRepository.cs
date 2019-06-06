using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class UserMigrationHistoryRepository : EfCoreBaseRepository<Guid, UserMigrationHistory>, IRepository<Guid, UserMigrationHistory>, IUserMigrationHistoryRepository
    {
        public UserMigrationHistoryRepository(IdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
