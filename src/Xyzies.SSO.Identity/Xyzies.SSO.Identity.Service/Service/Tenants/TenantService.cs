using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Services.Models;
using Xyzies.SSO.Identity.Services.Service.Relation;

namespace Xyzies.SSO.Identity.Services.Service.Tenants
{
    /// <inheritdoc />
    public class TenantService : ITenantService
    {
        private readonly IMemoryCache _memoryCache = null;
        private readonly IRelationService _httpService = null;

        /// <inheritdoc />
        public TenantService(IMemoryCache memoryCache,
            IRelationService httpService)
        {
            _memoryCache = memoryCache ??
                throw new ArgumentNullException(nameof(memoryCache));
            _httpService = httpService ??
                throw new ArgumentNullException(nameof(httpService));
        }

        public async Task<Guid?> GetByCompanyId(int companyId)
        {
            var tenants = await GetFromCache();
            return tenants.FirstOrDefault(x => x.Companies.Any(c => c.Id == companyId))?.Id;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TenantWithCompanies>> GetFromCache()
        {
            var cacheExpiration = _memoryCache.Get<DateTime>(Consts.Cache.TenantExpirationKey);
            var tenants = _memoryCache.Get<IEnumerable<TenantWithCompanies>>(Consts.Cache.TenantsKey);
            if (cacheExpiration < DateTime.Now || tenants?.Count() == 0)
            {
                await SetToCacheAsync();
                tenants = _memoryCache.Get<IEnumerable<TenantWithCompanies>>(Consts.Cache.TenantsKey);
            }
            return tenants ?? new List<TenantWithCompanies>();
        }

        /// <inheritdoc />
        public async Task SetToCacheAsync()
        {
            var tenants =  await _httpService.GetTenantsWithCompaniesAsync();

            _memoryCache.Set(Consts.Cache.TenantsKey, tenants);
            _memoryCache.Set(Consts.Cache.TenantExpirationKey, DateTime.Now.AddHours(1));
        }
    }
}
