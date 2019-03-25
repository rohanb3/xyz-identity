using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class UserRefreshOptions
    {
        [Required]
        public string refresh_token { get; set; }
    }
}
