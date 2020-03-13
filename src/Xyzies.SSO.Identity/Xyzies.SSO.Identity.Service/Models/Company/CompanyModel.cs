﻿using Newtonsoft.Json;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Models.Tenant;

namespace Xyzies.SSO.Identity.Services.Models.Company
{
    /// <summary>
    /// Company model
    /// </summary>
    public class CompanyModel : CompanySimpleModel
    {
        /// <summary>
        /// Status key from CP
        /// </summary>
        public RequestStatus RequestStatus { get; set; }

        /// <summary>
        /// Company Tenant
        /// </summary>
        public TenantModel Tenant { get; set; }
    }
}
