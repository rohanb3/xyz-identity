using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Core
{
    public class SearchParameters : LazyLoadParameters
    {
        public string Role { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Company { get; set; }
    }
}
