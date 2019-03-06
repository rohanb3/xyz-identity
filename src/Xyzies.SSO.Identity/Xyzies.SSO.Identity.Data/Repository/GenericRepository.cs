using System;
using Microsoft.EntityFrameworkCore;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
    public sealed class GenericRepository<TEntity> : EfCoreBaseRepository<Guid, TEntity>
        where TEntity : class, IEntity<Guid>
    {
        public GenericRepository(DbContext dbContext)
            : base(dbContext)
        {

        }
    }
}
