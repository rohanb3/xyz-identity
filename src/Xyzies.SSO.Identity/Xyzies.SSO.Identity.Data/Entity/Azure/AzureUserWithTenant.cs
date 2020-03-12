using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureUserWithTenant : AzureUser
    {
        public Guid? TenantId { get; set; }
    }
}
