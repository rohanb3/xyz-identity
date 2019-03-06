using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity.Relationship;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class Policy : BaseEntity<Guid>
    {
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Relation to linking table of many to many between role and policy
        /// </summary>
        public virtual ICollection<PolicyToRole> RelationToRole { get; set; }

        /// <summary>
        /// Relation to linking table of many to many between policy and permission
        /// </summary>
        public virtual ICollection<PermissionToPolicy> RelationToPermission { get; set; }

        [NotMapped]
        public virtual IEnumerable<Role> Roles { get => RelationToRole == null ? Enumerable.Empty<Role>() : RelationToRole.Select(rp => rp.Entity2); }

        [NotMapped]
        public virtual IEnumerable<Permission> Permissions { get => RelationToPermission == null ? Enumerable.Empty<Permission>() : RelationToPermission.Select(rp => rp.Entity1); }
}
}
