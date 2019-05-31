using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
using Xyzies.SSO.Identity.Services.Models.Branch;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Service.Relation;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services.Migrations
{
    public class MigrationService : IMigrationService
    {
        private object _lock = new object();
        private readonly ICpUsersRepository _cpUsersRepository;
        private readonly IRequestStatusRepository _requestStatusesRepository;
        private readonly ICpRoleRepository _cpRoleRepository;
        private readonly IAzureAdClient _azureClient;
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;
        private readonly ILocaltionService _locationService;
        private readonly IRelationService _relationService;
        private readonly ILogger _logger;

        public MigrationService(
            ILogger<MigrationService> logger,
            IAzureAdClient azureClient,
            IRoleRepository roleRepository,
            ICpUsersRepository cpUsersRepository,
            IRequestStatusRepository requestStatusesRepository,
            IUserService userService,
            IRelationService relationService,
            ILocaltionService locationService,
            ICpRoleRepository cpRoleRepository)
        {
            _cpUsersRepository = cpUsersRepository ?? throw new ArgumentNullException(nameof(cpUsersRepository));
            _requestStatusesRepository = requestStatusesRepository ?? throw new ArgumentNullException(nameof(requestStatusesRepository));
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _relationService = relationService ?? throw new ArgumentNullException(nameof(relationService));
            _cpRoleRepository = cpRoleRepository ?? throw new ArgumentNullException(nameof(cpRoleRepository));
        }
        
        [Obsolete("Will be deleted")]
        public async Task MigrateAzureToCPAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync(new UserIdentityParams()
                {
                    Role = Consts.Roles.OperationsAdmin
                });
                var roles = _roleRepository.Get().ToList();

                var newUsers = users.Result
                    .Where(user => user.CPUserId == null || user.CPUserId == 0)
                    .Select(user =>
                    {
                        var newUser = user.Adapt<User>();
                        newUser.Role = roles.FirstOrDefault(role => role.RoleName == user.Role)?.RoleId.ToString() ?? null;
                        return newUser;
                    });

                foreach (var newUser in newUsers)
                {
                    if (newUser.Email != null)
                    {
                        var cablePortalUser = await _cpUsersRepository.GetByAsync(user => user.Email == newUser.Email);

                        if (cablePortalUser == null)
                        {
                            await _cpUsersRepository.AddAsync(newUser);
                        }
                        else
                        {
                            var existUser = (await _userService.GetUserBy(u2 => u2.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value == newUser.Email));
                            var adaptedUser = newUser.Adapt<AzureUser>();
                            adaptedUser.CPUserId = cablePortalUser.Id;
                            adaptedUser.Role = roles.FirstOrDefault(role => int.TryParse(adaptedUser.Role, out int RoleId) && role.RoleId == RoleId)?.RoleName ?? "Anonymous";

                            await _azureClient.PatchUser(existUser.ObjectId, adaptedUser);
                        }
                    }
                }
                var emails = newUsers.Select(user => user.Email?.ToLower()).Where(email => !string.IsNullOrEmpty(email)).ToArray();

                if (emails.Length > 0)
                {
                    await MigrateCPToAzureAsync(new MigrationOptions()
                    {
                        Emails = emails
                    });
                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
            finally
            {
                await _userService.SetUsersCache();
            }
        }

        public async Task ReplaceRoleIdWithRoleName()
        {
            var users = await _userService.GetAllUsersAsync(new UserIdentityParams()
            {
                Role = Consts.Roles.OperationsAdmin
            });
            var roles = _roleRepository.Get().ToList();
            var filteredUsers = users.Result.Where(user => int.TryParse(user.Role, out int roleId));
            foreach (var user in filteredUsers)
            {
                user.Role = roles.FirstOrDefault(role => int.TryParse(user.Role, out int RoleId) && role.RoleId == RoleId)?.RoleName ?? "Anonymous";
                await _userService.UpdateUserByIdAsync(user.ObjectId, user.Adapt<BaseProfile>());

                _logger.LogInformation($"User updated, {user.GivenName} {user.Surname} {user.Role ?? "NULL ROLE!!!"}");
            }
        }

        public async Task MigrateCPToAzureAsync(MigrationOptions options)
        {
            try
            {
                List<State> usersState = new List<State>();
                List<City> usersCity = new List<City>();

                var users = await _cpUsersRepository.GetAsync(user => user.IsDeleted != true);
                var roles = await _roleRepository.GetAsync();
                var statuses = await _requestStatusesRepository.GetAsync();
                var branches = await _relationService.GetBranchesTrustedAsync();

                IList<User> usersList;
                IList<Role> rolesList;
                IList<BranchModel> branchesList;
                IEnumerable<IGrouping<int?,BranchModel>> branchesByCompanies;
                IList<RequestStatus> statusesList;

                if (options.Emails?.Length > 0)
                {
                    users = users.Where(user => !string.IsNullOrEmpty(user.Email)).Where(user => options.Emails.Select(email => email.ToLower()).Contains(user.Email.ToLower()));
                }
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());

                lock (_lock)
                {
                    branchesList = branches.ToList();
                    branchesByCompanies = branches.GroupBy(branch => branch.CompanyId);
                    usersList = users.ToList();
                    rolesList = roles.ToList();
                    statusesList = statuses.ToList();
                }

                foreach (var user in usersList)
                {
                    try
                    {
                        var companyBranches = branchesByCompanies.FirstOrDefault(branchGroup => branchGroup.Key == user.CompanyId);
                        PrepeareUserProperties(user, rolesList, statusesList, companyBranches);

                        await _azureClient.PostUser(user.Adapt<AzureUser>());
                        HandleUserProperties(usersState, usersCity, user);

                        _logger.LogInformation($"New user, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"} offset {options.Offset}");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "User already exist")
                        {
                            var existUser = (await _userService.GetUserBy(u => u.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value.ToLower() == user.Email.ToLower()));
                            var adaptedUser = user.Adapt<AzureUser>();

                            await _azureClient.PatchUser(existUser.ObjectId, adaptedUser);
                            HandleUserProperties(usersState, usersCity, user);

                            _logger.LogInformation($"User updated, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"} offset {options.Offset}");
                        }
                    }

                }
                lock (_lock)
                {
                    _locationService.SetState(usersState).Wait();
                    _locationService.SetCity(usersCity).Wait();
                }
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                await _userService.SetUsersCache();
            }
        }

        public async Task FillNullRolesWithAnonymous()
        {
            var users = (await _userService.GetAllUsersAsync(new UserIdentityParams { Role = Consts.Roles.OperationsAdmin })).Result.Where(user => user.Role == null);

            foreach (var user in users)
            {
                await _azureClient.PatchUser(user.ObjectId, new AzureUser { Role = "Anonyumous" });
                _logger.LogInformation($"Role filled, {user.DisplayName}");
            }
        }
        public async Task SetAllEmailsToLowerCase(MigrationOptions options)
        {
            var users = await _userService.GetAllUsersAsync(new UserIdentityParams { Role = Consts.Roles.OperationsAdmin });
            var lazyLoadedUsers = users.Result.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Result.Count());
            foreach (var user in lazyLoadedUsers)
            {
                if (user.Email != null)
                {
                    await _azureClient.PatchUser(user.ObjectId, new AzureUser { SignInNames = new List<SignInName> { new SignInName { Type = "emailAddress", Value = user.Email.ToLower() } } });
                    _logger.LogInformation($"Sign in name reset, {user.Email.ToLower()}");
                }
                else
                {
                    _logger.LogError("NULL EMAIL!!!");
                }
            }
        }
        public async Task UpdateUserActivityStatus(MigrationOptions options = null)
        {
            try
            {
                var users = await _cpUsersRepository.GetAsync(x => x.IsDeleted != true);
                var statuses = await _requestStatusesRepository.GetAsync();

                if (options.Emails.Length > 0)
                {
                    users = users.Where(user => options.Emails.Select(email => email.ToLower()).Contains(user.Email.ToLower()));
                }
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());

                foreach (var user in users.ToList())
                {
                    var existUser = await _userService.GetUserBy(u => u.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value == user.Email);
                    if (existUser == null)
                    {
                        _logger.LogWarning($"User does not exist in Azure, {user.Name} {user.LastName}");
                        await MigrateCPToAzureAsync(new MigrationOptions() { Emails = new[] { user.Email } });
                    }
                    else
                    {
                        var isActive = IsUserActive(user, statuses);
                        await _userService.UpdateUserByIdAsync(existUser.ObjectId, new BaseProfile { AccountEnabled = isActive });

                        _logger.LogInformation($"Account activity updated, {user.Name} {user.LastName}, active: {isActive}");
                    }

                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }

        public async Task FillSuperAdminsWithDefaultBranches(string token, MigrationOptions options = null)
        {
            var identity = new UserIdentityParams() { Role = Consts.Roles.OperationsAdmin };
            var filters = new UserFilteringParams() { Role = new List<string>() { Consts.Roles.SuperAdmin } };
            var users = await _userService.GetAllUsersAsync(identity, filters);
            var companies = await _relationService.GetCompanies(token);

            var superAdmins = users.Result;
            if (options.Emails.Length > 0)
            {
                superAdmins = superAdmins.Where(user => !string.IsNullOrEmpty(user.Email)).Where(user => options.Emails.Select(email => email.ToLower()).Contains(user.Email.ToLower()));
            }
            superAdmins = superAdmins.Skip(options?.Offset ?? 0).Take(options?.Limit ?? superAdmins.Count());

            var testCompanyResult = companies.Where(company => !string.IsNullOrWhiteSpace(company.Name)).FirstOrDefault(company => company.Name.ToLower().Contains("test company"));
            var testCompanyId = testCompanyResult?.Id;

            var companyIds = superAdmins.Select(sa => sa.CompanyId).Distinct().ToList();
            companyIds.Add(testCompanyId);

            var branches = await _relationService.GetBranchesAsync(token);
            var filteredBranches = branches.Where(branch => companyIds.Contains(branch.CompanyId));

            foreach (var superAdmin in superAdmins)
            {
                superAdmin.BranchId = filteredBranches.FirstOrDefault(branch => branch.CompanyId == superAdmin.CompanyId)?.Id;
                await _userService.UpdateUserByIdAsync(superAdmin.ObjectId, superAdmin.Adapt<BaseProfile>());
                _logger.LogInformation("Successfully updated branch for {UserName}, {BranchId}", superAdmin.DisplayName, superAdmin.BranchId);
            }

            var adminsWithoutCompany = superAdmins.Where(sa => !sa.CompanyId.HasValue || !companies.Any(company => company.Id == sa.CompanyId));
            foreach (var adminWithoutCompany in adminsWithoutCompany)
            {
                adminWithoutCompany.CompanyId = testCompanyId;
                adminWithoutCompany.BranchId = filteredBranches.FirstOrDefault(branch => branch.CompanyId == adminWithoutCompany.CompanyId)?.Id;
                await _userService.UpdateUserByIdAsync(adminWithoutCompany.ObjectId, adminWithoutCompany.Adapt<BaseProfile>());
                _logger.LogInformation("Successfully updated branch for {UserName}, {BranchId}", adminWithoutCompany.DisplayName, adminWithoutCompany.BranchId);
            }
        }

        /// <inheritdoc />
        public async Task ChangeRoleName()
        {
            _logger.LogInformation($"Begin updating users role from operator to support admin");

            var identity = new UserIdentityParams() { Role = Consts.Roles.OperationsAdmin };
            var filter = new UserFilteringParams { Role = new List<string> { Consts.Roles.Operator } };
            var users = await _userService.GetAllUsersAsync(identity, filter);
            _logger.LogInformation($"Count of users in Azure - {users.Total}");
            var operatorRole = await _cpRoleRepository.GetByAsync(x => x.RoleName == Consts.Roles.Operator);
            var supAdminRoleId = (await _cpRoleRepository.GetByAsync(x => x.RoleName == Consts.Roles.SupportAdmin)).RoleId;
            try
            {
                var cpUsers = (await _cpUsersRepository.GetAsync(x => x.Role == operatorRole.RoleId.ToString())).ToList();
                _logger.LogInformation($"Count of users in CP - {cpUsers.Count}");
                if (cpUsers.Any())
                {
                    foreach (var cpUser in cpUsers)
                    {
                        cpUser.RoleId = supAdminRoleId;
                        await _cpUsersRepository.UpdateAsync(cpUser);
                    }
                }
            }
            catch
            {
                _logger.LogInformation($"Users in CP were migrated");
            }
            if (users.Result.Any())
            {
                foreach (var user in users.Result)
                {
                    var newUser = new BaseProfile { Role = Consts.Roles.SupportAdmin };
                    await _userService.UpdateUserByIdAsync(user.ObjectId, newUser);
                }
            }
            _logger.LogInformation($"Finish updating users role from operator to support admin");
            try
            {
                _logger.LogInformation($"Begin removing role operator from CP");
                await _cpRoleRepository.RemoveAsync(operatorRole);
                _logger.LogInformation($"End removing role operator from CP");
            }
            catch
            {
                _logger.LogWarning($"Operator has already deleted from CP");
            }
        }

        #region Helpers
        private void HandleUserProperties(List<State> usersState, List<City> usersCity, User user)
        {
            if (usersState.FirstOrDefault(x => x?.Name?.ToLower() == user?.State?.ToLower()) == null && !string.IsNullOrEmpty(user?.State))
            {
                usersState.Add(new State { Name = user?.State, ShortName = user?.State });
            }

            if (usersCity.FirstOrDefault(x => x?.Name?.ToLower() == user?.City?.ToLower()) == null && !string.IsNullOrEmpty(user?.City) && !string.IsNullOrEmpty(user?.State))
            {
                usersCity.Add(new City { Name = user?.City, State = new State { Name = user?.State } });
            }
        }
        private void PrepeareUserProperties(User user, IEnumerable<Role> roles, IEnumerable<RequestStatus> statuses, IEnumerable<BranchModel> companyBranches)
        {
            user.Role = roles.FirstOrDefault(role => role.RoleId == user.RoleId)?.RoleName ?? "Anonymous";
            if (user.City == "")
            {
                user.City = null;
            }

            if (user.State == "")
            {
                user.State = null;
            }

            user.IsActive = IsUserActive(user, statuses);

            user.BranchId = GetUserBranch(user, companyBranches);
        }

        private bool IsUserActive(User user, IEnumerable<RequestStatus> statuses) =>
            user.IsActive == true && (statuses.FirstOrDefault(status => status.Id == user.UserStatusKey)?.Name.ToLower().Contains("approved") ?? false);
        private Guid? GetUserBranch(User user, IEnumerable<BranchModel> companyBranches) =>
            user.BranchId.HasValue ? user.BranchId : companyBranches.FirstOrDefault()?.Id;
        #endregion
    }
}