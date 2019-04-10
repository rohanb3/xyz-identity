using System;
using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class PasswordResetRequest : BaseEntity<Guid>
    {
        public override Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 4, MinimumLength = 4)]
        public string Code { get; set; }
    }
}
