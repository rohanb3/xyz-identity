using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Helpers;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service.Permission;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly AuthServiceOptions _options;

        public AuthService(IPermissionService permissionService, IUserService userService, IOptionsMonitor<AuthServiceOptions> authServiceOptionsMonitor)
        {
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
            _options = authServiceOptionsMonitor.CurrentValue ?? throw new ArgumentNullException(nameof(authServiceOptionsMonitor));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
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
            options.Username = options.Username.ToLower();

            var user = await _userService.GetUserBy(u => u.SignInNames.Any(n => n.Value.ToLower() == options.Username));
            if (user == null)
            {
                throw new ArgumentException(ErrorReponses.UserDoesNotExits);
            }

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


        private List<KeyValuePair<string, string>> BaseOptions
        {
            get => new List<KeyValuePair<string, string>> {
                 new KeyValuePair<string, string>("scope", $"openid offline_access {_options.Scope}"),
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
            result.Add(new KeyValuePair<string, string>("refresh_token", options.RefreshToken));

            return result;
        }

    }
}
