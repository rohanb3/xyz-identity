using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class TokenResponse
    {
        public string Token_type { get; set; }
        public string Expires_in { get; set; }
        public string Access_token { get; set; }
        public string Refresh_token { get; set; }
    }
}
