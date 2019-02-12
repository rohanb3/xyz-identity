﻿using IdentityServiceClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServiceClient.Service
{
    public class ClientService : IClientService
    {
        //TODO: check answer
        private readonly IdentityServiceClientOptions _options;
        public ClientService(IdentityServiceClientOptions options)
        {
            _options = options;
        }

        public Task<bool> CheckHash(string hash)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>Sequence of users</returns>
        public async Task<ResponseModel<List<Profile>>> GetAllUsersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.GraphApi.UserEntity}");
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel<List<Profile>>>(content);
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
                var response = await client.GetAsync($"{_options.ServiceUrl}/{Const.GraphApi.UserEntity}/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new KeyNotFoundException("User with current identifier does not exist");
                }
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResponseModel<Profile>>(content);
            }
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        public async Task DeleteByIdAsync(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{_options.ServiceUrl}/{Const.GraphApi.UserEntity}/{id}");
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException("User with current identifier does not exist");
                }
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new AccessViolationException();
                }
            }
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <returns></returns>
        public async Task CreateNewUserAsync(ProfileCreatable model)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{_options.ServiceUrl}/{Const.GraphApi.UserEntity}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("Can not create user with current parameters");
                }
            }
        }

        /// <summary>
        /// Upsert user fields that containse in model
        /// </summary>
        /// <returns></returns>
        public async Task UpsertUserAsync(string id, BaseProfile model)
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PatchAsync($"{_options.ServiceUrl}/{Const.GraphApi.UserEntity}", content);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException("Can not create user with current parameters");
                }
            }
        }
    }
}
