using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class PasswordResetRequestRepository : EfCoreBaseRepository<Guid, PasswordResetRequest>, IRepository<Guid, PasswordResetRequest>, IPasswordResetRequestRepository
    {
        public PasswordResetRequestRepository(IdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}
