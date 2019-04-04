using Microsoft.EntityFrameworkCore;
using System;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Relationship;

namespace Xyzies.SSO.Identity.Data
{
    public class CablePortalIdentityDataContext : DbContext
    {
        public CablePortalIdentityDataContext(DbContextOptions<CablePortalIdentityDataContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
