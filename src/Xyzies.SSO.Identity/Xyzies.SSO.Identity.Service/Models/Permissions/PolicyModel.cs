using System;
using System.Collections.Generic;

namespace Xyzies.SSO.Identity.Services.Models.Permissions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PolicyModel
    {
        public Guid PolicyId { get; set; }

        public string PolicyName { get; set; }

        public IList<ScopeModel> Scopes { get; set; } = new List<ScopeModel>();
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
