using Newtonsoft.Json;
using System.Collections.Generic;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.Data.Entity.Azure
{
    public class AzureUser
    {
        public string CreationType { get; set; }

        public List<SignInName> SignInNames { get; set; }

        public PasswordProfile PasswordProfile { get; set; }

        public string PasswordPolicies { get; set; }

        public string ObjectId { get; set; }

        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        [JsonProperty(Consts.PhonePropertyName)]
        public string Phone { get; set; }

        public string Status { get; set; }

        [JsonProperty(Consts.RolePropertyName)]
        public string Role { get; set; }

        [JsonProperty(Consts.AvatarUrlPropertyName)]
        public string AvatarUrl { get; set; }

        [JsonProperty(Consts.BranchIdPropertyName)]
        public string BranchId { get; set; }

        public string City { get; set; }

        [JsonProperty(Consts.CompanyIdPropertyName)]
        public string CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string MailNickname { get; set; }

        public string UsageLocation { get; set; }

        public bool? AccountEnabled { get; set; }
    }
}
