using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Core
{
    public class LazyLoadedResult<T> : LazyLoadParameters where T : class
    {
        public IEnumerable<T> Result { get; set; }
    }
}
