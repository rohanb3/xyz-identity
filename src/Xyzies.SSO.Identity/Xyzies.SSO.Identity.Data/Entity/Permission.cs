using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class Permission : BaseEntity<Guid>
    {
        [Required]
        public string Scope { get; set; }

        public bool IsActive { get; set; }

        [NotMapped]
        public virtual ICollection<Policy> Policies { get; set; }
    }
}
