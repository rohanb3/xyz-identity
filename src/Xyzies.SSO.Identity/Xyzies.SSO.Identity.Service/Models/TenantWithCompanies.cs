using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xyzies.SSO.Identity.Service.Models;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Models
{
    /// <summary>
    /// Tenant with short information by companies
    /// </summary>
    public class TenantWithCompanies : Tenant
    {
        public TenantWithCompanies()
        {
            Companies = new List<CompanySimpleModel>();
        }

        public IEnumerable<CompanySimpleModel> Companies { get; set; }
    }
}
