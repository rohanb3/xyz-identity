using System;
using System.ComponentModel.DataAnnotations;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class BaseProfile
    {
        [StringLength(64)]
        public string DisplayName { get; set; }

        [StringLength(64)]
        public string GivenName { get; set; }

        [StringLength(64)]
        public string Surname { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        public string State { get; set; }

        public virtual int? CompanyId { get; set; }

        public int? RetailerId { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Status { get; set; }

        public RequestStatus RequestStatus { get; set; }

        public string Role { get; set; }

        public string AvatarUrl { get; set; }

        public Guid? BranchId { get; set; }

        public Guid? StatusId { get; set; }

        public Guid? DepartmentId { get; set; }

        public bool? AccountEnabled { get; set; }

        [StringLength(36)]
        public string MailNickname { get; set; }

        [StringLength(32)]
        public string UsageLocation { get; set; }
    }
}
