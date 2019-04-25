using Mapster;
using Microsoft.Extensions.Caching.Memory;
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

namespace Xyzies.SSO.Identity.UserMigration.Services
{
    public class MigrationService : IMigrationService
    {
        private readonly ICpUsersRepository _cpUsersRepository;
        private readonly IAzureAdClient _azureClient;
        private readonly IUserService _userService;
        private readonly IRoleRepository _roleRepository;
        private readonly ILocaltionService _locationService;


        public MigrationService(IMemoryCache cache, ICpUsersRepository cpUsersRepository, IAzureAdClient azureClient, IRoleRepository roleRepository, IUserService userService, ILocaltionService locationService)
        {
            _cpUsersRepository = cpUsersRepository ?? throw new ArgumentNullException(nameof(cpUsersRepository));
            _azureClient = azureClient ?? throw new ArgumentNullException(nameof(azureClient));
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task MigrateAsync(MigrationOptions options)
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

                        Console.WriteLine($"New user, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"}");
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "User already exist")
                        {
                            var existUser = (await _userService.GetUserBy(u => u.SignInNames.FirstOrDefault(name => name.Type == "emailAddress")?.Value == user.Email));
                            var adaptedUser = user.Adapt<AzureUser>();

                            await _azureClient.PatchUser(existUser.ObjectId, adaptedUser);
                            HandleUserProperties(usersState, usersCity, user);

                            Console.WriteLine($"User updated, {user.Name} {user.LastName} {user.Role ?? "NULL ROLE!!!"}");
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
                Console.WriteLine($"Role filled, {user.DisplayName}");
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
                    Console.WriteLine($"Sign in name reset, {user.Email.ToLower()}");
                }
                else
                {
                    Console.WriteLine("NULL EMAIL!!!");
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

                    Console.WriteLine($"Enable updated, {user.Name} {user.LastName}");

                }
            }
            catch (ApplicationException ex)
            {
                throw;
            }
        }

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

    }
}
