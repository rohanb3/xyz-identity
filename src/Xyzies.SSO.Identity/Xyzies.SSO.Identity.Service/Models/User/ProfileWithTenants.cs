using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class ProfileWithTenants : Profile
    {
        public Guid? TenantId { get; set; }
    }
}
