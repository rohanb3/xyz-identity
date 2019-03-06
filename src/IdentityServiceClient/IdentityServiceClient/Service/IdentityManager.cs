using IdentityServiceClient.Models;
using IdentityServiceClient.Models.User;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServiceClient.Service
{
    public class IdentityManager : IIdentityManager
    {
        //TODO: check answer

        private readonly IdentityServiceClientOptions _options;
        private readonly TimeSpan _expitationTime = new TimeSpan(1, 0, 0);

        public HttpContext Context { get; set; }
        public IdentityManager(IdentityServiceClientOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Sequence of users</returns>
        public async Task<ResponseModel<List<Profile>>> GetAllUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <returns>User</returns>
        public async Task<ResponseModel<Profile>> GetUserByIdAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new KeyNotFoundException("User with current identifier does not exist");
                }
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<Profile>()
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "OK",
                    Payload = JsonConvert.DeserializeObject<Profile>(content)
                };
            }
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        public async Task<ResponseModel> DeleteUserByIdAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.DeleteAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}/{id}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException("User with current identifier does not exist");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new AccessViolationException();
                }
            }

            return new ResponseModel() { StatusCode = HttpStatusCode.OK, Message = "User successefully deleted" };
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel> CreateUserAsync(ProfileCreatable model)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("Can not create user with current parameters");
                }

                return new ResponseModel() { StatusCode = HttpStatusCode.OK, Message = "User successefully created" };
            }
        }

        /// <summary>
        /// Upsert user fields that containse in model
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseModel> UpdateUserByIdAsync(string id, BaseProfile model)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("Can not create user with current parameters");
                }

                return new ResponseModel() { StatusCode = HttpStatusCode.OK, Message = "User successefully updated" };
            }
        }

        /// <summary>
        /// Get all users with current role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<Profile>>> GetUsersByRole(string role)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}?role={role}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        /// <summary>
        /// Get all users with current managerId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<Profile>>> GetUsersByManager(string managerId)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}?managerId={managerId}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        /// <summary>
        /// Get all users with current managerIds
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<Profile>>> GetUsersByManagers(List<string> managerIds)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}?{GenerateQueryString(managerIds, "managerId")}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        /// <summary>
        /// Get all users with current companyId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<Profile>>> GetUsersByCompany(string companyId)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}?companyId={companyId}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        /// <summary>
        /// Get all users with current companyIds
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseModel<List<Profile>>> GetUsersByCompanies(List<string> companyIds)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.IndentityApi.UserEntity}?{GenerateQueryString(companyIds, "companyId")}");
                var content = await response.Content.ReadAsStringAsync();
                return new ResponseModel<List<Profile>>()
                {
                    StatusCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Payload = JsonConvert.DeserializeObject<List<Profile>>(content)
                };
            }
        }

        public async Task<bool> HasPermission(string role, string[] scopes)
        {
            using (HttpClient client = new HttpClient())
            {
                SetAuthHeader(client);
                var request = new HttpRequestMessage(HttpMethod.Head,
                    $"{_options.ServiceUrl}/{Const.IndentityApi.RoleEntity}?{GenerateQueryString(scopes.ToList(), "scope")}&role={role}");
                var response = await client.SendAsync(request);
                return response.StatusCode == HttpStatusCode.OK;
            }
        }

        private string GenerateQueryString(List<string> values, string parameter)
        {
            string query = "";
            foreach (var value in values)
            {
                query += $"{parameter}={value}{(value == values.Last() ? "" : "&")}";
            }
            return query;
        }

        private void SetAuthHeader(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("Authorization", $"{Context.Request.Headers["Authorization"]}");
        }
    }
}

