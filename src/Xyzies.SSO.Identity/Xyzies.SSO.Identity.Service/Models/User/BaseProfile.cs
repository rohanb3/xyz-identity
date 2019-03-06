using System.ComponentModel.DataAnnotations;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class BaseProfile
    {
        [Required]
        [StringLength(64)]
        public string DisplayName { get; set; }

        [StringLength(64)]
        public string GivenName { get; set; }

        [StringLength(64)]
        public string Surname { get; set; }

        [StringLength(64)]
        public string City { get; set; }

        public int? CompanyId { get; set; }

        public int? RetailerId { get; set; }

        [Phone]
        public string Phone { get; set; }

        public string Status { get; set; }

        public string Role { get; set; }

        public string AvatarUrl { get; set; }

        public bool? AccountEnabled { get; set; }

        [StringLength(32)]
        public string MailNickname { get; set; }

        [StringLength(32)]
        public string UsageLocation { get; set; }
    }
}
