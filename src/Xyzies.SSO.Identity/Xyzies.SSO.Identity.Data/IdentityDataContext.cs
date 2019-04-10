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

        public DbSet<City> City { get; set; }

        public DbSet<State> State { get; set; }

        public DbSet<PasswordResetRequest> PasswordResetRequest { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //var permissions = new[]
            //{
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.templates.read" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.templates.write" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.templates.update" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.templates.delete" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.reviews.read" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.reviews.write" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.reviews.update" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.reviews.reviews.delete" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.authorization.reviews.admin" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.authorization.reviews.mobile" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.authorization.vsp.operator" },
            //    new Permission { Id = Guid.NewGuid(), IsActive = true, Scope = "xyzies.authorization.vsp.mobile" },
            //};

            //var policies = new[]
            //{
            //    new Policy { Id = Guid.NewGuid(), Name = "TemplatesFull" },
            //    new Policy { Id = Guid.NewGuid(), Name = "ReviewsFull" },
            //    new Policy { Id = Guid.NewGuid(), Name = "ReviewsAdminLogin" },
            //    new Policy { Id = Guid.NewGuid(), Name = "ReviewsMobileLogin" },
            //    new Policy { Id = Guid.NewGuid(), Name = "VspOperatorLogin" },
            //    new Policy { Id = Guid.NewGuid(), Name = "VspMobileLogin" },
            //};

            //var roles = new[]
            //{
            //    new Role { Id = Guid.NewGuid(), RoleId = 1, RoleName = "SuperAdmin", IsCustom = false, CreatedOn = DateTime.Now },
            //    new Role { Id = Guid.NewGuid(), RoleId = 2, RoleName = "RetailerAdmin", IsCustom = false, CreatedOn = DateTime.Now },
            //    new Role { Id = Guid.NewGuid(), RoleId = 3, RoleName = "SalesRep", IsCustom = false, CreatedOn = DateTime.Now },
            //    new Role { Id = Guid.NewGuid(), RoleId = 4, RoleName = "Operator", IsCustom = false, CreatedOn = DateTime.Now },
            //};

            //builder.Entity<Permission>().HasData(permissions);
            //builder.Entity<Policy>().HasData(policies);
            //builder.Entity<Role>().HasData(roles);

            builder.Entity<PermissionToPolicy>().HasKey(x => new { x.Relation1Id, x.Relation2Id });
            builder.Entity<PolicyToRole>().HasKey(x => new { x.Relation1Id, x.Relation2Id });
            builder.Entity<City>().Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Entity<State>().Property(c => c.Id).ValueGeneratedOnAdd();

            base.OnModelCreating(builder);
        }
    }
}
