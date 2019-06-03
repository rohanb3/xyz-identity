using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public class RequestStatusRepository : EfCoreBaseRepository<Guid, RequestStatus>, IRepository<Guid, RequestStatus>, IRequestStatusRepository
    {
        public RequestStatusRepository(CablePortalIdentityDataContext dbContext)
            : base(dbContext)
        {

        }
    }
}