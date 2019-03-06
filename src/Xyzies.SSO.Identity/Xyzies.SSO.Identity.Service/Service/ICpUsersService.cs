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
        Task<LazyLoadedResult<CpUser>> GetAllCpUsers(string authorRole, string companyId, LazyLoadParameters lazyLoad = null);
        Task<CpUser> GetUserById(int id,int authorId, string authorRole, string companyId);
    }
}
