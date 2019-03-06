using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Core
{
    public class LazyLoadParameters
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
