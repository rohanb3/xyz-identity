using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xyzies.SSO.Identity.Data.Entity
{
    [Table("TWC_Users")]
    public class User : BaseEntity<int>
    {
        [Column("UserId")]
        public override int Id { get; set; }

        public int? CompanyId { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int? SalesPersonID { get; set; }

        public string Role { get; set; }

        [NotMapped]
        public int? RoleId => string.IsNullOrEmpty(Role) ? null : new int?(int.Parse(Role));

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        [Column("Active")]
        public bool? IsActive { get; set; }

        [Column("is_Agreement")]
        public bool? IsAgreement { get; set; }

        [Column("imagename")]
        public string ImageName { get; set; }

        public int? UserRefID { get; set; }

        public string XyziesId { get; set; }

        public int? ManagedBy { get; set; }

        [Column("deleted")]
        public bool? IsDeleted { get; set; }

        public Guid? UserGuid { get; set; }

        public string IPAddressRestriction { get; set; }

        public DateTime? PasswordExpiryOn { get; set; }

        public bool? IsPhoneVerified { get; set; }

        public bool? IsIdentityUploaded { get; set; }

        public bool? IsEmailVerified { get; set; }

        public int? StatusId { get; set; }
    }
}
