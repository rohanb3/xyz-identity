using Microsoft.EntityFrameworkCore;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data
{
    public class CablePortalIdentityDataContext : DbContext
    {
        public CablePortalIdentityDataContext(DbContextOptions<CablePortalIdentityDataContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<CPRole> Roles { get; set; }
    }
}
