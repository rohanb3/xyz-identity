using System;
using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class State : BaseEntity<Guid>
    {
        [Key]
        public override Guid Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
