using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.API.Models
{
    public class RequestVerificationCodeModel
    {
        /// <summary>
        /// Email to sent code
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
