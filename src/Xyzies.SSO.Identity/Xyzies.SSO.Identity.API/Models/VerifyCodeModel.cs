using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.API.Models
{
    public class VerifyCodeModel
    {
        /// <summary>
        /// Email to verify with current code
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Code to verify with current email
        /// </summary>
        [Required]
        [StringLength(maximumLength: 4, MinimumLength = 4)]
        public string Code { get; set; }
    }
}
