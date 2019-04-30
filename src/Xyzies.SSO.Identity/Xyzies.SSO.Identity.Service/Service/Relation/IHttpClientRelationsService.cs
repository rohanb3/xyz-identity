using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Models.Branch;
using Xyzies.SSO.Identity.Services.Models.Company;

namespace Xyzies.SSO.Identity.Services.Service.Relation
{
    /// <summary>
    /// Http clien for communication with microservices
    /// </summary>
    public interface IHttpClientRelationsService
    {
        /// <summary>
        /// Get company by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CompanyModel> GetCompanyById(int id);

        /// <summary>
        /// Get branch by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BranchModel> GetBranchById(Guid id);
    }
}
