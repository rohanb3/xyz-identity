using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
// Please, do not use this reference 
// Issue: https://stackoverflow.com/questions/30400358/cant-find-system-net-http-formatting-dll
// using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Entity.Azure;
using Xyzies.SSO.Identity.Data.Entity.Azure.AzureAdGraphApi;

namespace Xyzies.SSO.Identity.Data.Repository.Azure
{
    public class AzureAdClient : IAzureAdClient
    {
        private readonly AzureAdGraphApiOptions _azureAdGraphApiOptions;
        private readonly AzureAdB2COptions _azureAdB2COptions;
        private AzureAdApiCredentials _credentials;

        public AzureAdClient(IOptionsMonitor<AzureAdGraphApiOptions> azureAdGraphApiOptionsMonitor, IOptionsMonitor<AzureAdB2COptions> azureAdB2COptionsMonitor)
        {
            _azureAdGraphApiOptions = azureAdGraphApiOptionsMonitor?.CurrentValue ??
                throw new ArgumentNullException(nameof(azureAdGraphApiOptionsMonitor));
            _azureAdB2COptions = azureAdB2COptionsMonitor?.CurrentValue ??
                throw new ArgumentNullException(nameof(azureAdB2COptionsMonitor));
        }

        public async Task DeleteUser(string id)
        {
            var response = await SendRequest(HttpMethod.Delete, Consts.GraphApi.UserEntity, additional: id);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("User with current identifier does not exist");
            }
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new AccessViolationException();
            }
        }

        public async Task<AzureUser> GetUserById(string id)
        {
            var response = await SendRequest(HttpMethod.Get, Consts.GraphApi.UserEntity, additional: id);
            var responseString = await response?.Content?.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject(responseString) as JToken;
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("User with current identifier does not exist");
            }

            return value.ToObject<AzureUser>();
        }

        public async Task<IEnumerable<AzureUser>> GetUsers(string filter = null)
        {
            var response = await SendRequest(HttpMethod.Get, Consts.GraphApi.UserEntity, query: filter);
            var responseString = await response?.Content?.ReadAsStringAsync();
            var value = (JsonConvert.DeserializeObject(responseString) as JToken)["value"];

            return value.ToObject<List<AzureUser>>();
        }

        public async Task PatchUser(string id, AzureUser user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await SendRequest(HttpMethod.Patch, Consts.GraphApi.UserEntity, content, id);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Can not update user with current parameters");
            }
        }

        public async Task PostUser(AzureUser user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await SendRequest(HttpMethod.Post, Consts.GraphApi.UserEntity, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Can not create user with current parameters");
            }
        }

        private async Task<HttpResponseMessage> SendRequest(HttpMethod method, string entity, StringContent content = null, string additional = null, string query = "")
        {
            using (HttpClient httpClient = new HttpClient())
            {
                await SetClient(httpClient, $"{entity}{(string.IsNullOrEmpty(additional) ? string.Empty : $"/{additional}")}", query);
                return method == HttpMethod.Get ? await httpClient.GetAsync(httpClient.BaseAddress)
                     : method == HttpMethod.Delete ? await httpClient.DeleteAsync(httpClient.BaseAddress)
                     : method == HttpMethod.Post ? await httpClient.PostAsync(httpClient.BaseAddress, content)
                     : await httpClient.PatchAsync(httpClient.BaseAddress, content);
            }
        }

        // NOTE: The same as below
        private async Task SetClient(HttpClient client, string entity, string query = "")
        {
            var queryString = HttpUtility.ParseQueryString(query);
            queryString[Consts.GraphApi.ApiVersionParameter] = Consts.GraphApi.ApiVersion;
            client.BaseAddress = new Uri($"{Consts.GraphApi.GraphApiEndpoint}{_azureAdB2COptions.Domain}/{entity}?{queryString}");
            await SetCredentials(client);
        }

        // NOTE: Please, do not make functions like this in future
        // This feature is bad for testing.
        // You can implement this function like "clean function" named GetUserCreadentials
        private async Task SetCredentials(HttpClient client)
        {
            if (_credentials == null)
            {
                using (var httpClient = new HttpClient())
                {
                    var pairs = new List<KeyValuePair<string, string>>
                    {
                      new KeyValuePair<string, string>("client_id", _azureAdGraphApiOptions.ClientId),
                      new KeyValuePair<string, string>("client_secret", _azureAdGraphApiOptions.ClientSecret),
                      new KeyValuePair<string, string>("Resource", _azureAdGraphApiOptions.Resource),
                      new KeyValuePair<string, string>("grant_type", _azureAdGraphApiOptions.GrantType)
                    };

                    var content = new FormUrlEncodedContent(pairs);
                    HttpResponseMessage response = await httpClient.PostAsync(_azureAdGraphApiOptions.RequestUri, content);
                    // NOTE: 
                    // Please, do not use methods from System.Net.Http.Formatting
                    // There is an issue with build on linux
                    string buffer = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(buffer))
                    {
                        throw new InvalidOperationException("Couldn't retrieve auth data");
                    }

                    _credentials = JsonConvert.DeserializeObject<AzureAdApiCredentials>(buffer);
                    if (_credentials == null)
                    {
                        throw new InvalidOperationException("Couldn't retrieve auth data");
                    }
                }
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _credentials.AccessToken);
        }
    }
}
