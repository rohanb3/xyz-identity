using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class City : BaseEntity<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
    }
}
