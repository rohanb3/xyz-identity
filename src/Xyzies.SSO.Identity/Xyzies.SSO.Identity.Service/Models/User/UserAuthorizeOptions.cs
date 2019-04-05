using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    /// <summary>
    /// Represents data for user authorization
    /// </summary>
    public class UserAuthorizeOptions
    {
        /// <summary>
        /// Username or login
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// User scope
        /// </summary>
        [Required]
        public string Scope { get; set; }
    }
}
