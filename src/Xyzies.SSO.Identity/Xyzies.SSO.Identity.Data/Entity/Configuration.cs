using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class Configuration: BaseEntity<string>
    {
        [Column("Key")]
        public override string Id { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
