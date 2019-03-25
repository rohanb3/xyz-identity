using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class UserAuthorizeOptions
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Scope { get; set; }
    }
}
