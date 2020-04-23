using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.Tenant
{
    public class TenantBaseModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
