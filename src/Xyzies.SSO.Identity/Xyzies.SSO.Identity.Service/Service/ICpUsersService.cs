using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public interface ICpUsersService
    {
        Task<LazyLoadedResult<CpUser>> GetAllCpUsers(string authorId, string authorRole, string companyId, SearchParameters lazyLoad = null);
        Task<CpUser> GetUserById(int id, int authorId, string authorRole, string companyId);
    }
}
