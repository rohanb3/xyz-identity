using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xyzies.SSO.Identity.Data.Entity.Relationship
{
    public sealed class PolicyToRole : ManyToMany<Guid, Policy, Role>
    {
        /// <summary>
        /// Relation to Permission
        /// </summary>
        [Key, Column("PolicyId")]
        public override Guid Relation1Id { get => base.Relation1Id; set => base.Relation1Id = value; }

        /// <summary>
        /// Relation to Policy
        /// </summary>
        [Key, Column("RoleId")]
        public override Guid Relation2Id { get => base.Relation2Id; set => base.Relation2Id = value; }
    }
}
