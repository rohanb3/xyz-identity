using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity
{
    [Table("TWC_Role")]
    public class CPRole : BaseEntity<Guid>
    {
        [Column("RoleKey")]
        public override Guid Id { get; set; }
        public int RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
