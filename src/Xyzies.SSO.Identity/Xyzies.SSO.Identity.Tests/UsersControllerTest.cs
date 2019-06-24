//using System.Linq;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.TestHost;
//using System.Threading.Tasks;
//using Xunit;
//using Xyzies.SSO.Identity.Services.Models.User;
//using Xyzies.SSO.Identity.API;
//using System.IO;
//using Xyzies.SSO.Identity.Data.Helpers;
//using Newtonsoft.Json;
//using Microsoft.Extensions.Configuration;
//using System;
//using Microsoft.Extensions.DependencyInjection;
//using Xyzies.SSO.Identity.Data.Core;
//using Xyzies.SSO.Identity.Data.Repository.Azure;
//using Xyzies.SSO.Identity.Data.Entity.Azure;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using Microsoft.Extensions.Caching.Memory;
//using System.Net.Http.Headers;
//using Mapster;
//using Xyzies.SSO.Identity.Data.Entity;
//using FluentAssertions;

//namespace Xyzies.SSO.Identity.Tests
//{
//    public class UsersControllerTest : IClassFixture<TestServerInitializer>
//    {
//        private readonly IAzureAdClient _azureAdClient;
//        private readonly IMemoryCache _cache;
//        private readonly IServiceProvider _serviceProvider;
//        private TestServerInitializer _baseTest;

//        public UsersControllerTest(TestServerInitializer baseTest)
//        {
//            _baseTest = baseTest;
//            _serviceProvider = _baseTest.Server.Host.Services ?? throw new InvalidOperationException();
//            _azureAdClient = _serviceProvider.GetRequiredService<IAzureAdClient>();
//            _cache = _serviceProvider.GetRequiredService<IMemoryCache>();
//        }

//        #region GetUsers
//        [Theory(DisplayName = "ShouldReturnAllUsersForSuperAdmin")]
//        [InlineData(Consts.Roles.OperationsAdmin)]
//        [InlineData(Consts.Roles.SystemAdmin)]
//        [InlineData(Consts.Roles.AccountAdmin)]
//        public async Task ShouldReturnAllUsersForSuperAdmin(string role)
//        {
//            // Arrange
//            var createdUser = await CreateUser(role);
//            try
//            {
//                var token = await AuthorizeUser(createdUser.PasswordProfile.Password, createdUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var users = await GetUsers(token);
//                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.FirstOrDefault(x => x.ObjectId == createdUser.ObjectId) != null);
//                Assert.Contains(role, users.Result.Select(x => x.Role));
//                Assert.Equal(usersInCache.Count, users.Result.Count());
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//            }
//        }

//        [Theory(DisplayName = "ShouldReturnMe")]
//        [InlineData(Consts.Roles.SalesRep)]
//        public async Task ShouldReturnMe(string role)
//        {
//            // Arrange
//            var createdUser = await CreateUser(role);
//            try
//            {
//                var token = await AuthorizeUser(createdUser.PasswordProfile.Password, createdUser.Email, "xyzies.authorization.reviews.mobile");
//                // Act
//                var users = await GetUsers(token);
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Total == 1);
//                Assert.True(users.Result.FirstOrDefault(x => x.ObjectId == createdUser.ObjectId) != null);
//                Assert.True(users.Result.FirstOrDefault(x => x.ObjectId == createdUser.ObjectId).Role == role);
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//            }
//        }

//        [Theory(DisplayName = "ShouldReturnInMyCompany")]
//        [InlineData(Consts.Roles.SuperAdmin)]
//        public async Task ShouldReturnInMyCompany(string role)
//        {
//            // Arrange
//            var random = new Random();
//            var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
//            var companyId = usersInCache[random.Next(usersInCache.Count - 1)].CompanyId;
//            var createdUser = await CreateUser(role, companyId: companyId);
//            try
//            {
//                var token = await AuthorizeUser(createdUser.PasswordProfile.Password, createdUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var users = await GetUsers(token);
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.FirstOrDefault(x => x.ObjectId == createdUser.ObjectId) != null);
//                Assert.True(users.Result.All(x => x.CompanyId.ToString() == companyId));
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//            }
//        }

//        [Theory(DisplayName = "ShouldReturnMyProfile")]
//        [InlineData(Consts.Roles.SalesRep, "xyzies.authorization.reviews.mobile")]
//        [InlineData(Consts.Roles.OperationsAdmin, "xyzies.authorization.reviews.admin")]
//        [InlineData(Consts.Roles.SystemAdmin, "xyzies.authorization.reviews.admin")]
//        [InlineData(Consts.Roles.AccountAdmin, "xyzies.authorization.reviews.admin")]
//        [InlineData(Consts.Roles.SuperAdmin, "xyzies.authorization.reviews.admin")]
//        public async Task ShouldReturnMyProfile(string role, string scope)
//        {
//            // Arrange
//            var createdUser = await CreateUser(role);
//            try
//            {
//                var token = await AuthorizeUser(createdUser.PasswordProfile.Password, createdUser.Email, scope);
//                // Act
//                var user = (await GetProfile(token)).Adapt<AzureUser>();
//                // Assert
//                Assert.Equal(user.ObjectId, createdUser.ObjectId);
//                Assert.Equal(user.Email, createdUser.Email);
//                Assert.Equal(user.Role, createdUser.Role);
//                Assert.Equal(user.DisplayName, createdUser.DisplayName);
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//            }
//        }
//        #endregion

//        #region UpdateUser
//        [Theory(DisplayName = "ShouldUpdateUser")]
//        [InlineData(Consts.Roles.OperationsAdmin)]
//        public async Task ShouldUpdateUser(string role)
//        {
//            // Arrange
//            var createdUser = await CreateUser(role);
//            try
//            {
//                var token = await AuthorizeUser(createdUser.PasswordProfile.Password, createdUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var user = await GetUserById(token, createdUser.ObjectId);
//                var newRole = role + Guid.NewGuid();
//                user.Role = newRole;
//                var updatingResponse = await UpdateUser(token, createdUser.ObjectId, user.Adapt<BaseProfile>());
//                var updated = await GetUserById(token, createdUser.ObjectId);
//                // Assert
//                Assert.True(updatingResponse.IsSuccessStatusCode);
//                Assert.NotEqual(user, updated);
//                Assert.Equal(updated.Role, newRole);
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//            }
//        }
//        #endregion

//        #region GetFilteredUsers
//        [Theory(DisplayName = "ShouldReturnFilteredByRoles")]
//        [InlineData(new string[] { Consts.Roles.SalesRep }, Consts.Roles.OperationsAdmin)]
//        [InlineData(new string[] { Consts.Roles.OperationsAdmin, Consts.Roles.SalesRep }, Consts.Roles.SystemAdmin)]
//        public async Task ShouldReturnFilteredByRoles(string[] roles, string userRole)
//        {
//            // Arrange
//            var actualUser = await CreateUser(userRole);
//            var createdUsers = new List<AzureUser>();
//            foreach (var role in roles)
//            {
//                var createdUser = await CreateUser(role);
//                createdUsers.Add(createdUser);
//            }
//            try
//            {
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.Role)}=" + string.Join($"&{nameof(UserFilteringParams.Role)}=", roles));
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.DoesNotContain(users.Result, x => !createdUsers.Select(c => c.Role.ToLower()).Contains(x.Role?.ToLower()));
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//                foreach (var user in createdUsers)
//                {
//                    await DeleteUser(user.ObjectId);
//                }
//            }
//        }

//        [Theory(DisplayName = "ShouldReturnQuicklySearchedUsers")]
//        [InlineData("test_userName", Consts.Roles.SystemAdmin)]
//        public async Task ShouldReturnQuicklySearchedUsers(string userName, string userRole)
//        {
//            // Arrange
//            var guidInName = Guid.NewGuid().ToString().Replace("-", "");
//            userName = userName.Replace("_", guidInName);
//            var actualUser = await CreateUser(userRole);
//            var createdUser = await CreateUser(userRole, userName);
//            try
//            {
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.UserName)}={userName}");
//                var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.Count() == 1);
//                Assert.True(users.Result.FirstOrDefault(x => x.DisplayName.Contains(guidInName)) != null);
//            }
//            finally
//            {
//                await DeleteUser(createdUser.ObjectId);
//                await DeleteUser(actualUser.ObjectId);
//            }

//        }

//        [Fact(DisplayName = "ShouldReturnUsersByIds")]
//        public async Task ShouldReturnUsersByIds()
//        {
//            // Arrange
//            int usersCount = 5;
//            var actualUser = await CreateUser(Consts.Roles.OperationsAdmin);
//            var createdUsersIds = new List<string>();
//            try
//            {
//                for (int i = 0; i < usersCount; i++)
//                {
//                    var createdUser = await CreateUser(Consts.Roles.SalesRep);
//                    createdUsersIds.Add(createdUser.ObjectId);
//                }
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.UsersId)}=" + string.Join($"&{nameof(UserFilteringParams.UsersId)}=", createdUsersIds));
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.Count() == 5);
//                Assert.True(users.Result.All(x => createdUsersIds.Contains(x.ObjectId)));
//                Assert.DoesNotContain(users.Result, x => !createdUsersIds.Contains(x.ObjectId));
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//                foreach (var userId in createdUsersIds)
//                {
//                    await DeleteUser(userId);
//                }
//            }
//        }

//        [Fact(DisplayName = "ShouldReturnUsersByCompanyIds")]
//        public async Task ShouldReturnUsersByCompanyIds()
//        {
//            // Arrange
//            var actualUser = await CreateUser(Consts.Roles.OperationsAdmin);
//            try
//            {

//            var random = new Random();
//            var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//            var usersCompanyIds = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey).Select(x => x.CompanyId).Skip(random.Next(1000)).Take(random.Next(10)).Distinct().ToList();
//            // Act
//            var users = await GetUsers(token, $"{nameof(UserFilteringParams.CompanyId)}=" + string.Join($"&{nameof(UserFilteringParams.CompanyId)}=", usersCompanyIds));
//            // Assert

//            Assert.True(users.Result.Any());
//            Assert.True(users.Result.FirstOrDefault(x => !usersCompanyIds.Contains(x.CompanyId.ToString())) == null);
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//            }
//        }

//        [Fact(DisplayName = "ShouldReturnUsersByBranchIds")]
//        public async Task ShouldReturnUsersByBranchIds()
//        {
//            // Arrange
//            var actualUser = await CreateUser(Consts.Roles.OperationsAdmin);
//            try
//            {
//                var random = new Random();
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                var usersBranchIds = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey).Where(x => x.BranchId.HasValue).Select(x => x.BranchId);
//                usersBranchIds = usersBranchIds.Skip(random.Next(usersBranchIds.Count())).Take(10).Distinct();
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.BranchesId)}=" + string.Join($"&{nameof(UserFilteringParams.BranchesId)}=", usersBranchIds));
//                // Assert

//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.All(x => usersBranchIds.Contains(x.BranchId)));

//                Assert.DoesNotContain(users.Result, x => !usersBranchIds.Contains(x.BranchId));
//                Assert.Equal(usersBranchIds.Count(), users.Result.Select(x => x.BranchId).Distinct().Count());
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//            }
//        }

//        [Fact(DisplayName = "ShouldReturnUsersByCities")]
//        public async Task ShouldReturnUsersByCities()
//        {
//            // Arrange
//            var actualUser = await CreateUser(Consts.Roles.OperationsAdmin);
//            try
//            {
//                var random = new Random();
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                var usersCities = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey).Where(x => !string.IsNullOrEmpty(x.City)).Select(x => x.City.ToLower());
//                usersCities = usersCities.Skip(random.Next(usersCities.Count())).Take(10).Distinct();
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.City)}=" + string.Join($"&{nameof(UserFilteringParams.City)}=", usersCities));
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.FirstOrDefault(x => !usersCities.Contains(x.City.ToLower())) == null);
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//            }
//        }

//        [Fact(DisplayName = "ShouldReturnUsersByStates")]
//        public async Task ShouldReturnUsersByStates()
//        {
//            var actualUser = await CreateUser(Consts.Roles.OperationsAdmin);
//            try
//            {
//                // Arrange
//                var random = new Random(100);
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");
//                var usersStates = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey).Where(x => !string.IsNullOrEmpty(x.State)).Select(x => x.State.ToLower());
//                usersStates = usersStates.Skip(random.Next(usersStates.Count())).Take(10).Distinct();
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserFilteringParams.State)}=" + string.Join($"&{nameof(UserFilteringParams.State)}=", usersStates));
//                // Assert
//                Assert.True(users.Result.Any());
//                Assert.True(users.Result.FirstOrDefault(x => !usersStates.Contains(x.State.ToLower())) == null);
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//            }
//        }
//        #endregion

//        #region GetSortedUsers
//        [Theory(DisplayName = "ShouldReturnSortedUsers")]
//        [InlineData(Consts.UsersSorting.Id, Consts.UsersSorting.Ascending, nameof(AzureUser.ObjectId))]
//        [InlineData(Consts.UsersSorting.Id, Consts.UsersSorting.Descending, nameof(AzureUser.ObjectId))]
//        [InlineData(Consts.UsersSorting.Id, "", nameof(AzureUser.ObjectId))]

//        [InlineData(Consts.UsersSorting.Name, Consts.UsersSorting.Ascending, nameof(AzureUser.DisplayName))]
//        [InlineData(Consts.UsersSorting.Name, Consts.UsersSorting.Descending, nameof(AzureUser.DisplayName))]
//        [InlineData(Consts.UsersSorting.Name, "", nameof(AzureUser.DisplayName))]

//        [InlineData(Consts.UsersSorting.Role, Consts.UsersSorting.Ascending, nameof(AzureUser.Role))]
//        [InlineData(Consts.UsersSorting.Role, Consts.UsersSorting.Descending, nameof(AzureUser.Role))]
//        [InlineData(Consts.UsersSorting.Role, "", nameof(AzureUser.Role))]

//        [InlineData(Consts.UsersSorting.State, Consts.UsersSorting.Ascending, nameof(AzureUser.State))]
//        [InlineData(Consts.UsersSorting.State, Consts.UsersSorting.Descending, nameof(AzureUser.State))]
//        [InlineData(Consts.UsersSorting.State, "", nameof(AzureUser.State))]

//        [InlineData(Consts.UsersSorting.City, Consts.UsersSorting.Ascending, nameof(AzureUser.City))]
//        [InlineData(Consts.UsersSorting.City, Consts.UsersSorting.Descending, nameof(AzureUser.City))]
//        [InlineData(Consts.UsersSorting.City, "", nameof(AzureUser.City))]
//        public async Task ShouldReturnSortedUsers(string sortFied, string order, string fieldName)
//        {
//            // Arrange
//            var actualUser = await CreateUser(Consts.Roles.SystemAdmin);
//            try
//            {
//                var token = await AuthorizeUser(actualUser.PasswordProfile.Password, actualUser.Email, "xyzies.authorization.reviews.admin");

//                var usersValues = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey).Select(x => x.GetType().GetProperty(fieldName).GetValue(x));
//                var orderedValues = order == Consts.UsersSorting.Descending ? usersValues.OrderByDescending(x => x).ToList() : usersValues.OrderBy(x => x).ToList();
//                // Act
//                var users = await GetUsers(token, $"{nameof(UserSortingParameters.Sort)}={sortFied}&{nameof(UserSortingParameters.By)}={order}");
//                // Assert

//                Assert.True(users.Result.Any());
//                Assert.Equal(users.Result.Count(), usersValues.Count());
//                Assert.Equal(users.Result.Select(x => x.GetType().GetProperty(fieldName).GetValue(x)).ToList(), orderedValues);
//            }
//            finally
//            {
//                await DeleteUser(actualUser.ObjectId);
//            }
//        }
//        #endregion

//        #region Helpers
//        private async Task<string> AuthorizeUser(string userPassword, string userEmail, string scope)
//        {
//            var credentials = new UserAuthorizeOptions { Password = userPassword, Username = userEmail, Scope = scope };
//            var content = new StringContent(JsonConvert.SerializeObject(credentials), Encoding.UTF8, "application/json");
//            var authResponse = await _baseTest.HttpClient.PostAsync("api/authorize/token", content);
//            var responseString = await authResponse.Content.ReadAsStringAsync();
//            return JsonConvert.DeserializeObject<TokenResponse>(responseString).Access_token;
//        }

//        private async Task<AzureUser> CreateUser(string role, string userName = "", string companyId = null)
//        {
//            var userEmail = $"test{Guid.NewGuid()}@test.com".Replace("-", "");
//            var userPassword = "Secret12345";
//            var testUser = new AzureUser
//            {
//                AccountEnabled = true,
//                DisplayName = string.IsNullOrEmpty(userName) ? $"Test{Guid.NewGuid()}".Replace("-", "") : userName,
//                CreationType = "LocalAccount",
//                Role = role,
//                CompanyId = companyId,
//                PasswordProfile = new PasswordProfile { Password = userPassword, ForceChangePasswordNextLogin = false },
//                SignInNames = new List<SignInName> {
//                    new SignInName { Type = "emailAddress", Value = userEmail }
//                }
//            };
//            var createdUser = await _azureAdClient.PostUser(testUser);
//            var usersInCache = _cache.Get<List<AzureUser>>(Consts.Cache.UsersKey);
//            usersInCache.Add(createdUser);
//            _cache.Set(Consts.Cache.UsersKey, usersInCache);
//            createdUser.PasswordProfile = new PasswordProfile { Password = userPassword };
//            createdUser.Email = userEmail;
//            return createdUser;
//        }

//        private async Task<LazyLoadedResult<Profile>> GetUsers(string token, string query = "")
//        {
//            _baseTest.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            var response = await _baseTest.HttpClient.GetAsync($"api/users?{query}");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var users = JsonConvert.DeserializeObject<LazyLoadedResult<Profile>>(responseString);
//            return users;
//        }

//        private async Task<Profile> GetProfile(string token)
//        {
//            _baseTest.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            var response = await _baseTest.HttpClient.GetAsync("api/users/profile");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var user = JsonConvert.DeserializeObject<Profile>(responseString);
//            return user;
//        }

//        private async Task<Profile> GetUserById(string token, string id)
//        {
//            _baseTest.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            var response = await _baseTest.HttpClient.GetAsync($"api/users/{id}");
//            var responseString = await response.Content.ReadAsStringAsync();
//            var users = JsonConvert.DeserializeObject<Profile>(responseString);
//            return users;
//        }

//        private async Task<HttpResponseMessage> UpdateUser(string token, string id, BaseProfile profile)
//        {
//            _baseTest.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//            var content = new StringContent(JsonConvert.SerializeObject(profile), Encoding.UTF8, "application/json");
//            var response = await _baseTest.HttpClient.PatchAsync($"api/users/{id}", content);
//            return response;
//        }

//        private async Task DeleteUser(string id)
//        {
//            await _azureAdClient.DeleteUser(id);
//        }
//        #endregion
//    }
//}
