using Xyzies.SSO.Identity.Data.Core;

namespace Xyzies.SSO.Identity.Data.Entity
{
    public class UserFilteringParams: LazyLoadParameters
    {
        /// <summary>
        /// User role
        /// </summary>
        public string Role { get; set; }

        public bool? IsCablePortal { get; set; }

        public string CompanyId { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string UserName { get; set; }
    }
}
