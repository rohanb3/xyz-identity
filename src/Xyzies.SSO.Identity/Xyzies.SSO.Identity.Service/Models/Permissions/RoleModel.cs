using System;
using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Services.Models.Permissions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class RoleModel
    {
        public Guid RoleKey { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsCustomRole { get; set; }

        public IList<PolicyModel> Policies { get; set; } = new List<PolicyModel>();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
