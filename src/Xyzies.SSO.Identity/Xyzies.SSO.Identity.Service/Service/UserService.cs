using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class UserService : IUserService
    {
        private readonly IAzureAdClient _azureClient;
        private readonly ICpUsersRepository _cpUserRepo;
        private readonly IRoleRepository _roleRepo;
        private delegate bool UserFilters(User user);

        public UserService(IAzureAdClient azureClient, ICpUsersRepository cpUserRepo, IRoleRepository roleRepo)
        {
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _cpUserRepo = cpUserRepo ?? throw new ArgumentNullException(nameof(cpUserRepo));
            _roleRepo = roleRepo ?? throw new ArgumentNullException(nameof(roleRepo));
        }

        public async Task<LazyLoadedResult<Profile>> GetAllUsersAsync(UserIdentityParams user, UserFilteringParams filter = null)
        {
            if (user.Role == Consts.Roles.SuperAdmin)
            {
                if (!filter.IsCablePortal.HasValue)
                {
                    throw new ArgumentException("Value IsCablePortal must be specified");
                }
                if (filter.IsCablePortal.Value)
                {
                    var cpUsers = await _cpUserRepo.GetAsync();
                    await SetCpUsersRoles(cpUsers);
                    return GetFilteredUsers(cpUsers, filter).Adapt<LazyLoadedResult<Profile>>();
                }

                return await GetAzureUsers(filter);
            }

            if (user.Role == Consts.Roles.RetailerAdmin)
            {
                if (IsAzureUser(user.Id))
                {
                    filter.CompanyId = user.CompanyId;
                    return await GetAzureUsers(filter);
                }

                var cpUsers = await _cpUserRepo.GetAsync(x => x.CompanyId == int.Parse(user.CompanyId));
                await SetCpUsersRoles(cpUsers);
                return GetFilteredUsers(cpUsers, filter).Adapt<LazyLoadedResult<Profile>>();
            }

            if (user.Role == Consts.Roles.SalesRep)
            {
                if (IsAzureUser(user.Id))
                {
                    var salesRep = await _azureClient.GetUserById(user.Id.ToString());
                    return new LazyLoadedResult<Profile>()
                    {
                        Result = new List<Profile> { salesRep.Adapt<Profile>() },
                        Limit = 1,
                        Offset = 0,
                        Total = 1
                    };
                }
                var resultUser = await _cpUserRepo.GetAsync(x => x.CompanyId == int.Parse(user.CompanyId) && x.Id == int.Parse(user.Id));
                await SetCpUsersRoles(resultUser);
                return GetFilteredUsers(resultUser, filter).Adapt<LazyLoadedResult<Profile>>();
            }
            throw new ArgumentException("Unknown user role");
        }

        public async Task UpdateUserByIdAsync(string id, BaseProfile model)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                await _azureClient.PatchUser(id, model.Adapt<AzureUser>());
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<Profile> CreateUserAsync(ProfileCreatable model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                await _azureClient.PostUser(model.Adapt<AzureUser>());
                return model.Adapt<Profile>();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<Profile> GetUserByIdAsync(string id, UserIdentityParams user)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException("Id can not be null or empty");
                }

                if (user.Role == Consts.Roles.SalesRep && id != user.Id)
                {
                    throw new AccessException();
                }

                if (!IsAzureUser(id))
                {
                    if (user.Role == Consts.Roles.SuperAdmin)
                    {
                        var result = await _cpUserRepo.GetAsync(int.Parse(id));
                        result.Role = (_roleRepo.Get(x => x.RoleId == int.Parse(result.Role))).First().RoleName;
                        return result.Adapt<Profile>();
                    }

                    if (user.Role == Consts.Roles.RetailerAdmin || user.Role == Consts.Roles.SalesRep && !string.IsNullOrEmpty(user.CompanyId))
                    {
                        var result = await _cpUserRepo.GetByAsync(x => x.CompanyId == int.Parse(user.CompanyId) && x.Id == int.Parse(id));
                        result.Role = (_roleRepo.Get(x => x.RoleId == int.Parse(result.Role))).First().RoleName;
                        return result.Adapt<Profile>();
                    }

                    throw new AccessException();
                }
                else
                {
                    if (user.Role == Consts.Roles.SuperAdmin)
                    {
                        var result = await _azureClient.GetUserById(id);
                        return result.Adapt<Profile>();
                    }

                    if (user.Role == Consts.Roles.RetailerAdmin || user.Role == Consts.Roles.SalesRep && !string.IsNullOrEmpty(user.CompanyId))
                    {
                        var result = await _azureClient.GetUserById(id);
                        if (result.CompanyId != user.CompanyId)
                        {
                            throw new AccessException();
                        }
                        return result.Adapt<Profile>();
                    }

                    throw new AccessException();
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

        public async Task DeleteUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Object id can not be null or empty");
            }

            try
            {
                await _azureClient.DeleteUser(id);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (AccessViolationException)
            {
                throw;
            }
        }

        private bool IsAzureUser(string userId)
        {
            return !int.TryParse(userId, out int result);
        }

        private async Task<LazyLoadedResult<Profile>> GetAzureUsers(UserFilteringParams filter = null)
        {
            var azureUsers = await _azureClient.GetUsers(FilterConditions.GetUserFilterString(filter));
            return new LazyLoadedResult<Profile>()
            {
                Limit = filter.Limit,
                Total = azureUsers.Count(),
                Offset = filter.Offset,
                Result = azureUsers.Adapt<IEnumerable<Profile>>()
            };
        }

        private async Task SetCpUsersRoles(IQueryable<User> users)
        {
            var roles = (await _roleRepo.GetAsync()).ToList();
            foreach (var user in users)
            {
                user.Role = roles.FirstOrDefault(r => r.RoleId == user.RoleId)?.RoleName;
            }
        }

        private LazyLoadedResult<User> GetFilteredUsers(IQueryable<User> query, UserFilteringParams parameters)
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

            if (parameters.CompanyId != null)
            {
                filters += (User user) => user.CompanyId == int.Parse(parameters.CompanyId);
            }

            if (parameters.UserName != null)
            {
                filters += (User user) =>
                {
                    if(string.IsNullOrEmpty(user.Name) && string.IsNullOrEmpty(user.LastName))
                    {
                        return false;
                    }

                    if (!string.IsNullOrEmpty(user.Name) && string.IsNullOrEmpty(user.LastName))
                    {
                        return user.Name.ToLower().Contains(parameters.UserName.ToLower());
                    }

                    if (string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.LastName))
                    {
                        return user.LastName.ToLower().Contains(parameters.UserName.ToLower());
                    }

                    return user.LastName.ToLower().Contains(parameters.UserName.ToLower()) || user.Name.ToLower().Contains(parameters.UserName.ToLower());
                };
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
