using System.Collections.Generic;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Models.Tenant
{
    /// <summary>
    /// Tenant with short information by companies
    /// </summary>
    public class TenantWithCompaniesModel : TenantModel
    {
        public TenantWithCompaniesModel()
        {
            Companies = new List<CompanySimpleModel>();
        }

        public IEnumerable<CompanySimpleModel> Companies { get; set; }
    }
}
