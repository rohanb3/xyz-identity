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
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.UserMigration.Models;

namespace Xyzies.SSO.Identity.UserMigration.Services.Migrations
{
    public class MigrationService : IMigrationService
    {
        private readonly ICpUsersRepository _cpUsersRepository;
        private readonly IAzureAdClient _azureClient;
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;
        private readonly ILocaltionService _locationService;
        private readonly ILogger _logger;

        public MigrationService(ILogger<MigrationService> logger, ICpUsersRepository cpUsersRepository, IAzureAdClient azureClient, IRoleRepository roleRepository, IUserService userService, ILocaltionService locationService)
        {
            _cpUsersRepository = cpUsersRepository ?? throw new ArgumentNullException(nameof(cpUsersRepository));
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


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
            }
            catch (ApplicationException ex)
            {
                throw;
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
                var users = await _cpUsersRepository.GetAsync(x => x.IsDeleted != true);
                if (options.Emails.Length > 0)
                {
                    users = users.Where(user => options.Emails.Select(email => email.ToLower()).Contains(user.Email.ToLower()));
                }
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());
                var roles = (await _roleRepository.GetAsync()).ToList();

                foreach (var user in users.ToList())
                {
                    try
                    {
                        PrepeareUserProperties(user, roles);
                        await _azureClient.PostUser(user.Adapt<AzureUser>());
                        HandleUserProperties(usersState, usersCity, user);

                        _logger.LogInformation($"New user, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"}");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "User already exist")
                        {
                            var existUser = (await _userService.GetUserBy(u => u.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value == user.Email));
                            var adaptedUser = user.Adapt<AzureUser>();

                            await _azureClient.PatchUser(existUser.ObjectId, adaptedUser);
                            HandleUserProperties(usersState, usersCity, user);

                            _logger.LogInformation($"User updated, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"}");
                        }
                    }

                }
                await _locationService.SetState(usersState);
                await _locationService.SetCity(usersCity);
            }
            catch (ApplicationException ex)
            {
                throw;
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
        public async Task SyncEnabledUsers(MigrationOptions options)
        {
            try
            {
                var users = await _cpUsersRepository.GetAsync(x => x.IsDeleted != true);
                if (options.Emails.Length > 0)
                {
                    users = users.Where(user => options.Emails.Select(email => email.ToLower()).Contains(user.Email.ToLower()));
                }
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());

                foreach (var user in users.ToList())
                {
                    var existUser = await _userService.GetUserBy(u => u.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value == user.Email);
                    if (user.IsActive == true)
                    {
                        await _userService.UpdateUserByIdAsync(existUser.ObjectId, new Identity.Services.Models.User.BaseProfile { AccountEnabled = user.IsActive ?? false });
                    }

                    _logger.LogInformation($"Enable updated, {user.Name} {user.LastName}");

                }
            }
            catch (ApplicationException ex)
            {
                throw;
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
        private void PrepeareUserProperties(User user, List<Role> roles)
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
        }

        public async Task PeriodicTask()
        {
            _logger.LogInformation("Periodic task");
        }
        #endregion
    }
}