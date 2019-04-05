using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    /// <summary>
    /// 
    /// </summary>
    public class UserRefreshOptions
    {
        /// <summary>
        /// Token to allow refresh auth token
        /// </summary>
        [Required, JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
