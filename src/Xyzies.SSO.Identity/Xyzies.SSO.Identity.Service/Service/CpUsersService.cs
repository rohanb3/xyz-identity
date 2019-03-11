using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class CpUsersService : ICpUsersService
    {
        private readonly ICpUsersRepository _cpUserRepo;
        private readonly IRoleRepository _roleRepo;
        private delegate bool UserFilters(User user);

        public CpUsersService(ICpUsersRepository cpUserRepo, IRoleRepository roleRepo)
        {
            _cpUserRepo = cpUserRepo;
            _roleRepo = roleRepo;
        }

        public async Task<LazyLoadedResult<CpUser>> GetAllCpUsers(string authorId, string authorRole, string companyId, SearchParameters parameters = null)
        {
            if (authorRole == Consts.Roles.SuperAdmin)
            {
                var users = await _cpUserRepo.GetAsync();
                return GetFilteredUsers(users, parameters).Adapt<LazyLoadedResult<CpUser>>();
            }

            if (authorRole == Consts.Roles.RetailerAdmin && !string.IsNullOrEmpty(companyId))
            {
                var companyUsers = await _cpUserRepo.GetAsync(x => x.CompanyId == int.Parse(companyId));
                return GetFilteredUsers(companyUsers, parameters).Adapt<LazyLoadedResult<CpUser>>();
            }

            if (authorRole == Consts.Roles.SalesRep && !string.IsNullOrEmpty(companyId))
            {
                var user = (await _cpUserRepo.GetAsync(x => x.CompanyId == int.Parse(companyId) && x.Id == int.Parse(authorId))).GetPart(parameters);
                return user.Adapt<LazyLoadedResult<CpUser>>();
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

        private LazyLoadedResult<User> GetFilteredUsers(IQueryable<User> query, SearchParameters parameters)
        {
            UserFilters filters = null;
            if (parameters.Role != null)
            {
                var roleId = (_roleRepo.Get(x => x.RoleName == parameters.Role)).First().RoleId;
                filters += (User user) => user.Role == roleId.ToString();
            }

            if (parameters.State != null)
            {
                filters += (User user) => user.State == parameters.State;
            }

            if (parameters.City != null)
            {
                filters += (User user) => user.City == parameters.City;
            }

            if (parameters.Company != null)
            {
                filters += (User user) => user.CompanyId == int.Parse(parameters.Company);
            }

            if (filters != null)
            {
                query = query.Where(user => AllTrue(filters, user));
            }

            return query.GetPart(parameters);
        }

        private bool AllTrue(UserFilters condition, User user)
        {
            foreach (UserFilters t in condition.GetInvocationList())
            {
                if (!t(user))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
