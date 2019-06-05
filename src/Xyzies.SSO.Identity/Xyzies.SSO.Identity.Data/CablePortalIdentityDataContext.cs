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

        #region Entities
        public DbSet<User> Users { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<CPRole> Roles { get; set; }
        
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RequestStatus>().HasKey(entity => new { entity.Id, entity.WfKey });

            base.OnModelCreating(builder);
        }
    }
}
