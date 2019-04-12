using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.API.Models
{
    /// <summary>
    /// Response with token, that allows to reset password
    /// </summary>
    public class ResetTokenResponse
    {
        /// <summary>
        /// Reset token
        /// </summary>
        [Required]
        public string ResetToken { get; set; }
    }
}
