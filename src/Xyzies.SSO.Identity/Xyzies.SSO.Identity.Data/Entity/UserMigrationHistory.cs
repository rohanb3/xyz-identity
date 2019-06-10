using System;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class UserMigrationHistory : BaseEntity<Guid>
    {
        public DateTime CreatedOn { get; set; }
    }
}
