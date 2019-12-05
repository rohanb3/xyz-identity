using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xyzies.SSO.Identity.Data.Repository;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Helpers;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service.Permission;
using Xyzies.SSO.Identity.Services.Service.Relation;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;

namespace Xyzies.SSO.Identity.Services.Service
{
    public class AuthService : IAuthService
    {
        private readonly IPermissionService _permissionService;
        private readonly IUserService _userService;
        private readonly IRelationService _relationService;
        private readonly IRequestStatusRepository _requestStatusesRepository;
        private readonly AuthServiceOptions _options;
        private readonly ILogger _logger;

        public AuthService(IPermissionService permissionService, IRequestStatusRepository requestStatusesRepository, IRelationService relationService, ILogger<AuthService> logger, IUserService userService, IOptionsMonitor<AuthServiceOptions> authServiceOptionsMonitor)
        {
            _permissionService = permissionService ??
                throw new ArgumentNullException(nameof(permissionService));
            _relationService = relationService ??
                throw new ArgumentNullException(nameof(relationService));
            _options = authServiceOptionsMonitor.CurrentValue ??
                throw new ArgumentNullException(nameof(authServiceOptionsMonitor));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));
            _requestStatusesRepository = requestStatusesRepository ??
                throw new ArgumentNullException(nameof(requestStatusesRepository));
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
                _logger.LogError("Error while requesting azure endpoint, {message}, {JSON}", responseString, JsonConvert.SerializeObject(response));

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

            _logger.LogError("Checking user status on authorization");
            var userStatus = (await _requestStatusesRepository.GetByAsync(x => x.Id == user.StatusId))?.Name;
            if (userStatus == null || !userStatus.ToLower().Contains("approved"))
            {
                throw new AccessException("Check your status");
            }

            _logger.LogError("Getting token");
            var result = await RequestAzureEndpoint(new FormUrlEncodedContent(GetKeyValuePairOptions(options)));
            var jwtToken = new JwtSecurityToken(result.Access_token);
            var companyId = jwtToken.Claims.FirstOrDefault(claim => claim.Type == CompanyIdClaimType)?.Value;
            var roleName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == RoleClaimType)?.Value ??
                throw new ArgumentNullException("Can't get role");

            _logger.LogError("Checking permissions");
            await _permissionService.CheckPermissionExpiration();
            var hasPermissions = _permissionService.CheckPermission(roleName, new string[] { options.Scope });

            if (int.TryParse(companyId, out int parsedCompanyId))
            {
                _logger.LogError("Checking company");
                var company = await _relationService.GetCompanyById(parsedCompanyId, result.Access_token);
                if (!(company?.RequestStatus?.Name?.ToLower().Contains("onboarded") ?? false))
                {
                    throw new AccessException("There is some problems with your company");
                }
                if (company.Tenant == null)
                {
                    throw new AccessException("Tenant is not specified for your Company. Please, contact support");
                }
            }
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
            get => new List<KeyValuePair<string, string>>
            {
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
