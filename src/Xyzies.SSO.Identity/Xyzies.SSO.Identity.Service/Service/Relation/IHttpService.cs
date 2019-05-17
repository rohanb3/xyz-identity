using System;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models.Branch;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Service.Relation
{
    /// <summary>
    /// Http clien for communication with microservices
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<CompanyModel> GetCompanyById(int id, string token = null);

        /// <summary>
        /// Get branch by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BranchModel> GetBranchById(Guid id, string token = null);
    }
}
