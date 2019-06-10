using System;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public interface IUserMigrationHistoryRepository : IRepository<Guid, UserMigrationHistory>, IDisposable
    {
    }
}
