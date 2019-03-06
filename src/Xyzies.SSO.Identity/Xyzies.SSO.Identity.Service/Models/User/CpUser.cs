using System;
using System.Collections.Generic;
using System.Text;

namespace Xyzies.SSO.Identity.Services.Models.User
{
    public class CpUser
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public int? SalesPersonID { get; set; }

        public string Role { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public bool? IsActive { get; set; }

        public string Avatar { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
