using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Models;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service.Permission;
using Xyzies.SSO.Identity.Services.Service.Relation;

namespace Xyzies.SSO.Identity.Services.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IAzureAdClient _azureClient;
        private readonly IMemoryCache _cache;
        private readonly ILocaltionService _localtionService;
        private readonly IRelationService _httpService = null;
        private readonly IRoleRepository _roleRepository = null;
        private readonly IRequestStatusRepository _requestStatusRepository = null;
        private readonly IPermissionService _permissionService = null;
        private readonly ICpUsersRepository _cpUsersRep = null;
        private readonly string _projectUrl;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="azureClient"></param>
        /// <param name="cache"></param>
        /// <param name="localtionService"></param>
        /// <param name="httpService"></param>
        /// <param name="roleRepository"></param>
        /// <param name="permissionService"></param>
        /// <param name="requestStatusRepository"></param>
        /// <param name="options"></param>
        public UserService(IAzureAdClient azureClient,
            IMemoryCache cache,
            ILocaltionService localtionService,
            IRelationService httpService,
            IRoleRepository roleRepository,
            IPermissionService permissionService,
            IRequestStatusRepository requestStatusRepository,
            ICpUsersRepository cpUsersRep,
            IOptionsMonitor<ProjectSettingsOption> options)
        {
            _azureClient = azureClient ??
                throw new ArgumentNullException(nameof(azureClient));
            _localtionService = localtionService ??
                throw new ArgumentNullException(nameof(localtionService));
            _cache = cache ??
                throw new ArgumentNullException(nameof(cache));
            _httpService = httpService ??
                throw new ArgumentNullException(nameof(httpService));
            _cpUsersRep = cpUsersRep ??
                throw new ArgumentNullException(nameof(cpUsersRep));
            _roleRepository = roleRepository ??
                throw new ArgumentNullException(nameof(roleRepository));
            _permissionService = permissionService ??
               throw new ArgumentNullException(nameof(permissionService));
            _requestStatusRepository = requestStatusRepository ??
               throw new ArgumentNullException(nameof(requestStatusRepository));
            _projectUrl = options.CurrentValue?.ProjectUrl ??
                throw new InvalidOperationException("Missing URL to Azure");
        }

        /// <inheritdoc />
        public async Task<ProfileSecure> GetOwnProfile(string userId)
        {
            var user = await GetUserBy(u => u.ObjectId == userId);
            var result = user.Adapt<ProfileSecure>();
            result.Scopes = await _permissionService.GetScopesByRole(user.Role);

            return result;
        }

        /// <inheritdoc />
        public async Task<LazyLoadedResult<Profile>> GetAllUsersAsync(UserIdentityParams user, UserFilteringParams filter = null, UserSortingParameters sorting = null)
        {
            await _permissionService.CheckPermissionExpiration();
            if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadAll }))
            {
                return await GetUsers(filter, sorting);
            }

            if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadInCompany }))
            {
                filter.CompanyId = new List<string> { user.CompanyId };

                return await GetUsers(filter, sorting);
            }

            if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadOnlyRequester }))
            {
                var salesRep = await GetUserByIdAsync(user.Id.ToString(), user);
                salesRep.AvatarUrl = FormUrlForDownloadUserAvatar(salesRep.ObjectId);

                return new LazyLoadedResult<Profile>()
                {
                    Result = new List<Profile> { salesRep.Adapt<Profile>() },
                    Limit = 1,
                    Offset = 0,
                    Total = 1
                };
            }

            throw new ArgumentException("Can not check you permission");
        }


        public Task<Profile> GetUserBy(Func<AzureUser, bool> predicate)
        {
            return Task.FromResult(_cache
                .Get<List<AzureUser>>(Consts.Cache.UsersKey)
                .FirstOrDefault(predicate)
                .Adapt<Profile>());
        }

        /// <inheritdoc />
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
                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
                var user = usersInCache.FirstOrDefault(x => x.ObjectId == id);
                if (user != null)
                {
                    MergeObjects(model.Adapt<AzureUser>(), user);
                    if (string.IsNullOrEmpty(model.State))
                    {
                        model.State = user.State;
                    }
                }
                else
                {
                    usersInCache.Add(model.Adapt<AzureUser>());
                }

                _cache.Set(Consts.Cache.UsersKey, usersInCache);

                if (!string.IsNullOrEmpty(model.City) && !string.IsNullOrEmpty(model.State))
                {
                    await _localtionService.SetCity(model.City, model?.State);
                    await _localtionService.SetState(model.State);
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<Profile> CreateUserAsync(ProfileCreatable model, string token)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                var azureUser = model.Adapt<AzureUser>();

                if (await IsUserExist(GetUserEmail(azureUser)))
                {
                    throw new ArgumentException("User already exist", "User");
                }
                if (!string.IsNullOrWhiteSpace(model.Role))
                {
                    await ValidateUserRole(model);
                    await ValidationUserByRole(model, token);
                }

                var createdUser = await _azureClient.PostUser(model.Adapt<AzureUser>());

                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
                usersInCache.Add(createdUser);
                _cache.Set(Consts.Cache.UsersKey, usersInCache);
                if (!string.IsNullOrEmpty(model.State))
                {
                    await _localtionService.SetState(model.State);
                }
                if (!string.IsNullOrEmpty(model.City) && !string.IsNullOrEmpty(model.State))
                {
                    await _localtionService.SetCity(model.City, model.State);
                }

                return createdUser.Adapt<Profile>();
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task UpdateUserPasswordAsync(string userMail, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    throw new ArgumentException("invalid password", nameof(password));
                }

                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
                var userToChange = usersInCache.FirstOrDefault(user => GetUserEmail(user)?.ToLower() == userMail.ToLower()) ?? throw new KeyNotFoundException();

                MergeObjects(new ProfileCreatable
                {
                    PasswordProfile = new PasswordProfile
                    {
                        EnforceChangePasswordPolicy = false,
                        ForceChangePasswordNextLogin = false,
                        Password = password
                    }

                }, userToChange);

                userToChange.PasswordPolicies = Consts.PasswordPolicy.DisablePasswordExpirationAndStrong;

                await _azureClient.PatchUser(userToChange.ObjectId, userToChange);
                if (userToChange.CPUserId.HasValue)
                {
                    var cpUser = await _cpUsersRep.GetByAsync(x => x.Id == userToChange.CPUserId);
                    if (cpUser != null)
                    {
                        cpUser.Password = password;
                        await _cpUsersRep.UpdateAsync(cpUser);
                    }
                }

                usersInCache.RemoveAll(user => user.ObjectId == userToChange.ObjectId);
                usersInCache.Add(userToChange);

                _cache.Set(Consts.Cache.UsersKey, usersInCache);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == "Can not update user with current parameters")
                {
                    throw new ArgumentException("Not valid password");
                }

                throw ex;
            }
        }

        /// <inheritdoc />
        public async Task<Profile> GetUserByIdAsync(string id, UserIdentityParams user)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadOnlyRequester })
                    && id != user.Id)
                {
                    throw new AccessException();
                }

                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
                if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadAll }))
                {
                    var result = usersInCache.FirstOrDefault(x => x.ObjectId == id) ?? throw new KeyNotFoundException("User not found");
                    return result?.Adapt<Profile>();
                }

                if (_permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadInCompany })
                    || _permissionService.CheckPermission(user.Role, new string[] { Consts.UsersReadPermission.ReadOnlyRequester })
                    && !string.IsNullOrEmpty(user.CompanyId))
                {
                    var result = usersInCache.FirstOrDefault(x => x.ObjectId == id) ?? throw new KeyNotFoundException("User not found");
                    if (result == null && result?.CompanyId != user.CompanyId)
                    {
                        throw new AccessException();
                    }

                    return await Task.FromResult(result.Adapt<Profile>());
                }

                throw new AccessException();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteUserByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("Object id can not be null or empty");
            }

            try
            {
                await _azureClient.DeleteUser(id);

                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
                usersInCache.RemoveAll(x => x.ObjectId == id);
                _cache.Set(Consts.Cache.UsersKey, usersInCache);
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (AccessViolationException)
            {
                throw new AccessException();
            }
        }

        /// <inheritdoc />
        public Dictionary<string, int> GetUsersCountInCompanies(List<string> companyIds, UserSortingParameters sorting, LazyLoadParameters lazyParameters)
        {
            var result = new Dictionary<string, int>();
            var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
            if (companyIds != null && companyIds.Any())
            {
                foreach (var companyId in companyIds)
                {
                    var usersInCompanyCount = usersInCache.Where(x => x.CompanyId == companyId).Count();
                    result.Add(companyId, usersInCompanyCount);
                }

                return result;
            }

            var grouped = usersInCache.GroupBy(x => x.CompanyId);
            if (sorting?.By == Consts.UsersSorting.Descending)
            {
                grouped = grouped.OrderByDescending(x => x.Count());
            }

            if (sorting?.By == Consts.UsersSorting.Ascending)
            {
                grouped = grouped.OrderBy(x => x.Count());
            }

            grouped = grouped.Where(x => x.Key != null)
                .Skip(lazyParameters?.Offset ?? 0)
                .Take(lazyParameters?.Limit ?? grouped.Count());

            foreach (var value in grouped)
            {
                result.Add(value.Key, value.Count());
            }

            return result;
        }

        /// <inheritdoc />
        public async Task SetUsersCache()
        {
            var usersToCache = new List<AzureUser>();
            var users = await _azureClient.GetUsers(userCount: 999);
            usersToCache.AddRange(users.Users);
            while (!string.IsNullOrEmpty(users.NextLink))
            {
                users = await _azureClient.GetUsers(users.NextLink.Split('?').LastOrDefault(), 999, true);
                usersToCache.AddRange(users.Users);
            }

            _cache.Set(Consts.Cache.UsersKey, usersToCache);
        }

        /// <inheritdoc />
        public async Task UploadAvatar(string userId, AvatarModel model)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                MemoryStream stream = new MemoryStream();
                await model.Avatar.CopyToAsync(stream);
                await _azureClient.UpdateAvatar(userId, stream.ToArray());
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task DeleteAvatar(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            try
            {
                await _azureClient.DeleteAvatar(userId, Array.Empty<byte>());
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<FileModel> GetAvatar(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            try
            {
                return await _azureClient.GetAvatar(userId);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        #region Helpers

        private static bool IsPropertyAccessible(PropertyInfo prop) => prop.CanRead && prop.CanWrite;

        private string FormUrlForDownloadUserAvatar(string userId) => $"{_projectUrl}/users/{userId}/avatar";

        private async Task<LazyLoadedResult<Profile>> GetUsers(UserFilteringParams filter = null, UserSortingParameters sorting = null)
        {
            var statuses = await _requestStatusRepository.GetAsync();
            var users = _cache.Get<IEnumerable<AzureUser>>(Consts.Cache.UsersKey)
                .Join(statuses, user => user.StatusId, status => status.Id, (user, status) =>
                {
                    user.RequestStatus = status;
                    return user;
                }).ToList();

            var searchedUsers = users.GetByParameters(filter, sorting);
            searchedUsers.ForEach(x => x.AvatarUrl = FormUrlForDownloadUserAvatar(x.ObjectId));

            return await Task.FromResult(new LazyLoadedResult<Profile>
            {
                Result = searchedUsers?.Adapt<IEnumerable<Profile>>(),
                Limit = filter?.Limit,
                Offset = filter?.Offset,
                Total = searchedUsers?.Count
            });
        }

        private static void MergeObjects<T1, T2>(T1 source, T2 destination)
        {
            Type t1Type = typeof(T1);
            Type t2Type = typeof(T2);

            IEnumerable<PropertyInfo> t1PropertyInfos =
                t1Type.GetProperties().Where(IsPropertyAccessible);
            IEnumerable<PropertyInfo> t2PropertyInfos =
                t2Type.GetProperties().Where(IsPropertyAccessible);

            foreach (PropertyInfo t1PropertyInfo in t1PropertyInfos)
            {
                var propertyValue = t1PropertyInfo.GetValue(source);
                if (propertyValue != null)
                {
                    foreach (PropertyInfo t2PropertyInfo in t2PropertyInfos)
                    {
                        if (t1PropertyInfo.Name == t2PropertyInfo.Name)
                        {
                            t2PropertyInfo.SetValue(destination, propertyValue);
                            break;
                        }
                    }
                }
            }
        }

        private async Task ValidationUserByRole(ProfileCreatable model, string token)
        {
            if (model.Role.ToLower() == Consts.Roles.SuperAdmin || model.Role.ToLower() == Consts.Roles.SupportAdmin)
            {
                await ValidationByCompany(model.CompanyId, token);
            }
            else if (model.Role.ToLower() == Consts.Roles.SalesRep)
            {
                await ValidationByCompany(model.CompanyId, token);
                await ValidationByBranche(model.BranchId, token);
            }
            else
            {
                if (model.CompanyId.HasValue)
                {
                    await ValidationByCompany(model.CompanyId, token);
                }
                if (model.BranchId.HasValue)
                {
                    await ValidationByBranche(model.BranchId, token);
                }

            }
        }

        private async Task ValidationByCompany(int? companyId, string token)
        {
            if (!companyId.HasValue)
            {
                throw new ApplicationException($"{nameof(companyId)} is required for this role");
            }
            var company = await _httpService.GetCompanyById(companyId.Value, token);
            if (company == null)
            {
                throw new ApplicationException($"Company with id: {companyId.Value.ToString()} is not exist");
            }
        }

        private async Task ValidationByBranche(Guid? branchId, string token)
        {
            if (!branchId.HasValue)
            {
                throw new ApplicationException($"{nameof(branchId)} is required for this role");
            }
            var branch = await _httpService.GetBranchById(branchId.Value, token);
            if (branch == null)
            {
                throw new ApplicationException($"Branch with id: {branchId.Value.ToString()} is not exist");
            }
            if (!branch.CompanyId.HasValue)
            {
                throw new ApplicationException($"Branch not connected anyone company");
            }
            var company = await _httpService.GetCompanyById(branch.CompanyId.Value, token);
            if (company == null)
            {
                throw new ApplicationException($"Company from branch is not exist");
            }
        }

        private async Task ValidateUserRole(ProfileCreatable model)
        {
            var role = await _roleRepository.GetByAsync(r => r.RoleName.ToLower() == model.Role.ToLower());

            if (role == null)
            {
                throw new ArgumentException("Unknown role", "Role");
            }
        }

        private async Task<bool> IsUserExist(string email)
        {
            var user = await GetUserBy(u => GetUserEmail(u)?.ToLower() == email.Trim().ToLower());

            return user != null;
        }

        private string GetUserEmail(AzureUser user) => user.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value;
        #endregion
    }
}
