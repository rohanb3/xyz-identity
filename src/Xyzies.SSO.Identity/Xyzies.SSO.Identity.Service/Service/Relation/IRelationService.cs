using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models;
using Xyzies.SSO.Identity.Services.Models.Branch;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Service.Relation
{
    /// <summary>
    /// Http clien for communication with microservices
    /// </summary>
    public interface IRelationService
    {
        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CompanyModel> GetCompanyById(int id, string token = null);

        /// <summary>
        /// Get company by passed filters
        /// </summary>
        /// <param name="token"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<List<CompanyModel>> GetCompanies(string token, CompanyFilters filters = null);

        /// <summary>
        /// Get tenants with companies
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IEnumerable<TenantWithCompanies>> GetTenantsWithCompaniesAsync();

        /// <summary>
        /// Get branch by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BranchModel> GetBranchById(Guid id, string token = null);

        /// <summary>
        /// Get branches
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BranchModel>> GetBranchesAsync(string token = null);

        /// <summary>
        /// Get branches by trusted token
        /// </summary>
        /// <returns></returns>
        Task<List<BranchModel>> GetBranchesTrustedAsync();

        /// <summary>
        /// Get companies by trusted token
        /// </summary>
        /// <returns></returns>
        Task<List<CompanyModel>> GetCompaniesTrustedAsync();

        /// <summary>
        /// Get branches by company by trusted token
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        Task<List<BranchModel>> GetBranchesByCompanyTrustedAsync(int companyId);
    }
}
