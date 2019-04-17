using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Helpers
{
    public class AuthServiceOptions
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string TokenEndpoint { get; set; }
        [Required]
        public string Scope { get; set; }
    }
}
