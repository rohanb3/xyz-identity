using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.API.Models
{
    /// <summary>
    /// Model to reset password by resetToken
    /// </summary>
    public class PasswordModel
    {
        /// <summary>
        /// Reset token to verify user
        /// </summary>
        [Required]
        public string ResetToken { get; set; }
        /// <summary>
        /// New password to update
        /// </summary>
        [Required]
        [StringLength(64, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
