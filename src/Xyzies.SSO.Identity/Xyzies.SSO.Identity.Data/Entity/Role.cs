using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Xyzies.SSO.Identity.Data.Entity.Relationship;

namespace Xyzies.SSO.Identity.Data.Entity
{
    [Table("TWC_Role")]
    public class Role : BaseEntity<Guid>
    {
        [Column("RoleKey")]
        public override Guid Id { get; set; }

        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsCustom { get; set; }

        /// <summary>
        /// Relation to linking table of many to many between role and policy
        /// </summary>
        public virtual ICollection<PolicyToRole> RelationToPolicy { get; set; }

        [NotMapped]
        public virtual IEnumerable<Policy> Policies { get => RelationToPolicy == null ? Enumerable.Empty<Policy>() : RelationToPolicy.Select(rp => rp.Entity1); }
    }
}
