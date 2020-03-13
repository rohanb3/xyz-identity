using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class TenantFilter
    {
        public TenantFilter()
        {
            TenantIds = new List<Guid>();
        }

        /// <summary>
        /// Filter by tenant id (GUID)
        /// </summary>
        public IEnumerable<Guid> TenantIds { get; set; }
    }
}
