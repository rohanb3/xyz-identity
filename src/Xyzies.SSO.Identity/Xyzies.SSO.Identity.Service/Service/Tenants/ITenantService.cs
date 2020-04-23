using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models.Tenant;

namespace Xyzies.SSO.Identity.Services.Service.Tenants
{
    /// <summary>
    /// Tenant service
    /// </summary>
    public interface ITenantService
    {
        /// <summary>
        /// Get tenants from cache
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TenantWithCompaniesModel>> GetFromCache();

        /// <summary>
        /// Add tenants to cache
        /// </summary>
        /// <returns></returns>
        Task SetToCacheAsync();

        /// <summary>
        /// Get tenant by company id
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<Guid?> GetByCompanyId(int companyId);
    }
}
