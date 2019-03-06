using Microsoft.EntityFrameworkCore;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Relationship;

namespace Xyzies.SSO.Identity.Data
{
    public class IdentityDataContext : DbContext
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options)
        {

        }

        #region Entities

        public DbSet<Role> Roles { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<PolicyToRole> PolicyToRole { get; set; }

        public DbSet<PermissionToPolicy> PermissionToPolicy { get; set; }

        public DbSet<User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PermissionToPolicy>().HasKey(x => new { x.Relation1Id, x.Relation2Id });
            builder.Entity<PolicyToRole>().HasKey(x => new { x.Relation1Id, x.Relation2Id });
        }
    }
}
