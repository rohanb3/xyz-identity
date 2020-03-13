using System;
using System.Collections.Generic;
using System.Text;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Models.Tenant
{
    public class TenantSimpleWithUsersModel : TenantBaseModel
    {
        public TenantSimpleWithUsersModel()
        {
            Users = new List<UserBaseModel>();
        }

        public IEnumerable<UserBaseModel> Users { get; set; }
    }
}
