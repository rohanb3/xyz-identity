using System.Collections.Generic;
using System.Linq;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class UserFilteringParams
    {
        public string Role { get; set; }

        public int?[] CompanyIds { get; set; }

        public int?[] ManagerIds { get; set; }
    }
}
