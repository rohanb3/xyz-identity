using IdentityServiceClient.Models;
using IdentityServiceClient.Models.User;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServiceClient.Service
{
    public interface IIdentityManager
    {
        HttpContext Context { get; set; }

        #region Users
        Task<ResponseModel<List<Profile>>> GetAllUsersAsync();
        Task<ResponseModel<Profile>> GetUserByIdAsync(string id);
        Task<ResponseModel> UpdateUserByIdAsync(string id, BaseProfile model);
        Task<ResponseModel> DeleteUserByIdAsync(string id);
        Task<ResponseModel> CreateUserAsync(ProfileCreatable model);
        Task<ResponseModel<List<Profile>>> GetUsersByRole(string role);
        Task<ResponseModel<List<Profile>>> GetUsersByManager(string managerId);
        Task<ResponseModel<List<Profile>>> GetUsersByManagers(List<string> managerIds);
        Task<ResponseModel<List<Profile>>> GetUsersByCompany(string companyId);
        Task<ResponseModel<List<Profile>>> GetUsersByCompanies(List<string> companyIds);
        #endregion

        #region Permission
        Task<bool> HasPermission(string role, string[] scopes);
        #endregion
    }
}
