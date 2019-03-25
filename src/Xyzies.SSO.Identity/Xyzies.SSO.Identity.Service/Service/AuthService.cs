using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Models.User;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;
using Microsoft.Extensions.Options;
using Xyzies.SSO.Identity.Services.Helpers;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Services.Service.Permission;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IPermissionService _permissionService;
        private readonly AuthServiceOptions _options;

        public AuthService(IPermissionService permissionService, IOptionsMonitor<AuthServiceOptions> authServiceOptionsMonitor)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _options = authServiceOptionsMonitor.CurrentValue ?? throw new ArgumentNullException(nameof(authServiceOptionsMonitor));
        }

        private List<KeyValuePair<string, string>> BaseOptions
        {
            get => new List<KeyValuePair<string, string>> {
                 new KeyValuePair<string, string>("scope", $"openid offline_access {Scopes.AzureAccessScope}"),
                 new KeyValuePair<string, string>("response_type", "id_token"),
                 new KeyValuePair<string, string>("client_id", _options.ClientId)
            };
        }

        private List<KeyValuePair<string, string>> GetKeyValuePairOptions(UserAuthorizeOptions options)
        {
            var result = BaseOptions;

            result.Add(new KeyValuePair<string, string>("password", options.Password));
            result.Add(new KeyValuePair<string, string>("username", options.Username));
            result.Add(new KeyValuePair<string, string>("grant_type", GrantTypes.Password));

            return result;
        }

        private List<KeyValuePair<string, string>> GetKeyValuePairOptions(UserRefreshOptions options)
        {
            var result = BaseOptions;

            result.Add(new KeyValuePair<string, string>("grant_type", GrantTypes.RefreshToken));
            result.Add(new KeyValuePair<string, string>("refresh_token", options.refresh_token));

            return result;
        }

        private async Task<TokenResponse> RequestAzureEndpoint(FormUrlEncodedContent content)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync(_options.TokenEndpoint, content);
                var responseString = await response?.Content?.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var value = JsonConvert.DeserializeObject(responseString) as JToken;
                    var tokenResponse = value.ToObject<TokenResponse>();

                    return tokenResponse;
                }

                throw new AccessException(responseString);
            }
        }

        public async Task<TokenResponse> AuthorizeAsync(UserAuthorizeOptions options)
        {
            var result = await RequestAzureEndpoint(new FormUrlEncodedContent(GetKeyValuePairOptions(options)));
            var jwtToken = new JwtSecurityToken(result.Access_token);
            var roleName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == RoleClaimType)?.Value ?? throw new ArgumentNullException("Can't get role");

            await _permissionService.CheckPermissionExpiration();
            var hasPermissions = _permissionService.CheckPermission(roleName, new string[] { options.Scope });
            if (hasPermissions)
            {
                return result;
            }

            throw new AccessException("You have not access for this scope");
        }

        public async Task<TokenResponse> RefreshAsync(UserRefreshOptions options)
        {
            return await RequestAzureEndpoint(new FormUrlEncodedContent(GetKeyValuePairOptions(options)));
        }
    }
}
