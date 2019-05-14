using System;
using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class ProfileSecure: Profile
    {
        /// <summary>
        /// Collection of user's avalible scopes
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }
    }
}
