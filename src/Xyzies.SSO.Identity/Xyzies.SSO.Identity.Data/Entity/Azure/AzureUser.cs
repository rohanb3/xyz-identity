using Newtonsoft.Json;
using System;
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
        public Guid? BranchId { get; set; }

        [JsonProperty(Consts.DepartmentIdPropertyName)]
        public Guid? DepartmentId { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [JsonIgnore]
        public RequestStatus RequestStatus { get; set; }

        [JsonProperty(Consts.CompanyIdPropertyName)]
        public string CompanyId { get; set; }

        [JsonProperty(Consts.CPUserIdPropertyName)]
        public int? CPUserId { get; set; }

        [JsonProperty(Consts.StatudIdPropertyName)]
        public Guid? StatusId { get; set; }

        public string CompanyName { get; set; }

        public string MailNickname { get; set; }

        public string UsageLocation { get; set; }

        public bool? AccountEnabled { get; set; }
    }
}
