using System;
using System.Collections.Generic;

namespace IdentityServiceClient.Models.Role
{
    public class RoleModel
    {
        public Guid RoleKey { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsCustomRole { get; set; }

        public IList<PolicyModel> Policies { get; set; } = new List<PolicyModel>();
    }
}
