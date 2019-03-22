using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Models.User;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IRoleRepository _roleRepository;

        public AuthService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        private List<KeyValuePair<string, string>> GetKeyValuePairOptions(string grant_type, object options)
        {
            var result = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("scope",$"openid offline_access {Consts.Scopes.AzureAccessScope}"),
                new KeyValuePair<string, string>("response_type","id_token"),
                new KeyValuePair<string, string>("client_id","969c11d1-47ae-4190-bf2e-035563bd9fd8")
            };

            switch (grant_type)
            {
                case GrantTypes.Password:
                    result.Add(new KeyValuePair<string, string>("username", "salesrep@test.com"));
                    result.Add(new KeyValuePair<string, string>("password", "Secret12345"));
                    result.Add(new KeyValuePair<string, string>("grant_type", GrantTypes.Password));
                    break;
                case GrantTypes.RefreshToken:
                    result.Add(new KeyValuePair<string, string>("refresh_token", "Secret12345"));
                    result.Add(new KeyValuePair<string, string>("grant_type", GrantTypes.RefreshToken));
                    break;
            }

            return result;
        }

        private async Task<TokenResponse> RequestAzureEndpoint(string grant_type, object options)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(GetKeyValuePairOptions(grant_type, options));
                var response = await httpClient.PostAsync("https://ardasdev.b2clogin.com/ardasdev.onmicrosoft.com/oauth2/v2.0/token?p=B2C_1_SiIn_ROPC", content);
                var responseString = await response?.Content?.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var value = JsonConvert.DeserializeObject(responseString) as JToken;
                    var tokenResponse = value.ToObject<TokenResponse>();

                    return tokenResponse;
                }


                throw new ApplicationException(responseString);
            }
        }

        public async Task<TokenResponse> AuthorizeAsync(UserAuthorizeOptions options)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var content = new FormUrlEncodedContent(GetKeyValuePairOptions(GrantTypes.Password, options));
                var response = await httpClient.PostAsync("https://ardasdev.b2clogin.com/ardasdev.onmicrosoft.com/oauth2/v2.0/token?p=B2C_1_SiIn_ROPC", content);
                var responseString = await response?.Content?.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var value = JsonConvert.DeserializeObject(responseString) as JToken;
                    var tokenResponse = value.ToObject<TokenResponse>();

                    return tokenResponse;
                }


                throw new ApplicationException(responseString);
            }
        }

        public async Task<TokenResponse> RefreshAsync(string refresh_token)
        {
            throw new NotImplementedException();
        }
    }
}
