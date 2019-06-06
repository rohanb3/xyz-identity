using System;
using System.Collections.Generic;
using Xyzies.SSO.Identity.Data.Core;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class UserFilteringParams : LazyLoadParameters
    {
        /// <summary>
        /// User role
        /// </summary>
        public List<string> Role { get; set; }

        public List<string> UsersId { get; set; }

        public List<string> CompanyId { get; set; }

        public List<Guid> BranchesId { get; set; }

        public List<string> City { get; set; }

        public List<string> State { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }
    }
}
