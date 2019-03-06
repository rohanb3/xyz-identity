using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class CpUsersService : ICpUsersService
    {
        private readonly ICpUsersRepository _cpUserRepo;

        public CpUsersService(ICpUsersRepository cpUserRepo)
        {
            _cpUserRepo = cpUserRepo;
        }

        public async Task<LazyLoadedResult<CpUser>> GetAllCpUsers(string authorRole, string companyId, LazyLoadParameters lazyLoad = null)
        {
            //if (authorRole == Consts.Roles.SuperAdmin)
            {
                var users = (await _cpUserRepo.GetAsync()).GetPart(lazyLoad);
                return users.Adapt<LazyLoadedResult<CpUser>>();
            }

            if (authorRole == Consts.Roles.RetailerAdmin && !string.IsNullOrEmpty(companyId))
            {
                var companyUsers = (await _cpUserRepo.GetAsync(x => x.CompanyId == int.Parse(companyId))).GetPart(lazyLoad);
                return companyUsers.Adapt<LazyLoadedResult<CpUser>>();
            }
            return null;
        }

        public async Task<CpUser> GetUserById(int id, int authorId, string authorRole, string companyId)
        {
            if (authorId != id && authorRole == Consts.Roles.SalesRep)
            {
                return null;
            }

            if (authorRole == Consts.Roles.SuperAdmin)
            {
                var user = await _cpUserRepo.GetAsync(id);
                return user.Adapt<CpUser>();
            }

            if (authorRole == Consts.Roles.RetailerAdmin || authorRole == Consts.Roles.SalesRep && !string.IsNullOrEmpty(companyId))
            {
                var companyUser = await _cpUserRepo.GetByAsync(x => x.CompanyId == int.Parse(companyId) && x.Id == id);
                return companyUser.Adapt<CpUser>();
            }
            return null;
        }
    }
}
