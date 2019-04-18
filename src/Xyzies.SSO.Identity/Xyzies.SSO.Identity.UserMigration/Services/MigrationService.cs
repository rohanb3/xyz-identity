using Mapster;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Data.Repository.Azure;
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
                var users = await _cpUsersRepository.GetAsync(x => !(x.IsDeleted ?? false));
                users = users.Skip(options?.Offset ?? 0).Take(options?.Limit ?? users.Count());

                var roles = (await _roleRepository.GetAsync()).ToList();

                foreach (var user in users.ToList())
                {
                    try
                    {

                        user.Role = roles.FirstOrDefault(role => role.RoleId == user.RoleId)?.RoleName;
                        if(user.City == "")
                        {
                            user.City = null;
                        }

                        if (user.State == "")
                        {
                            user.State = null;
                        }

                        await _azureClient.PostUser(user.Adapt<AzureUser>());

                        if (usersState.FirstOrDefault(x => x?.Name?.ToLower() == user?.State?.ToLower()) == null && !string.IsNullOrEmpty(user?.State))
                        {
                            usersState.Add(new State { Name = user?.State, ShortName = user?.State });
                        }

                        if (usersCity.FirstOrDefault(x => x?.Name?.ToLower() == user?.City?.ToLower()) == null && !string.IsNullOrEmpty(user?.City) && !string.IsNullOrEmpty(user?.State))
                        {
                            usersCity.Add(new City { Name = user?.City, State = new State { Name = user?.State } });
                        }

                        Console.WriteLine($"Success , {user.Name} {user.LastName}");
                    }
                    catch (Exception ex)
                    {
                        //if(ex.Message != "User already exist" && ex.Message != "Can not create user with current parameters\n One or more properties contains invalid values.")
                        //{
                        //    throw ex;
                        //}
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
    }
}
