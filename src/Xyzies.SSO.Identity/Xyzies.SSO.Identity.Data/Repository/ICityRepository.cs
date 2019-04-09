using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public interface ICityRepository : IRepository<Guid, City>, IDisposable
    {
    }
}
