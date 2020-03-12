using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class UserFilteringParamsWithTenant : UserFilteringParams
    {
        public Guid? TenantId { get; set; }
    }
}
