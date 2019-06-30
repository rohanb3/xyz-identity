using IdentityServiceClient.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace IdentityServiceClient.Filters
{
    public class AccessFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<AccessFilter> _logger = null;
        public string[] Scopes { get; set; }
        public IIdentityManager _manager;
        
        public AccessFilter(ILogger<AccessFilter> logger,
            params string[] scopes)
        {
            Scopes = scopes ??
                throw new ArgumentNullException(nameof(scopes));
            _logger = logger ??
                throw new ArgumentNullException(nameof(scopes));
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"Starting process to validate and verify access token.");
            _manager = context.HttpContext.RequestServices.GetService<IIdentityManager>();
            var bearerToken = (context.HttpContext.Request.Headers[Const.Auth.AuthHeader]).ToString();
            if (string.IsNullOrEmpty(bearerToken))
            {
                Console.WriteLine($"Access token is missing.");
                context.Result = new ContentResult { StatusCode = 401 };
            }

            bearerToken = bearerToken.Substring(Const.Auth.BearerToken.Length);
            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadJwtToken(bearerToken);
            var role = tokenS.Claims.FirstOrDefault(claim => claim.Type == Const.Permissions.RoleClaimType)?.Value;

            foreach (var scope in Scopes)
            {
                var scopes = scope.Split(',');
                var hashPermission = await _manager.HasPermission(role, scopes);
                if (!hashPermission)
                {
                    Console.WriteLine($"Not enough permissions");
                    context.Result = new ContentResult { StatusCode = 403 };
                }
                else
                {
                    Console.WriteLine($"Access token is verified successfully");
                    await next();
                }
            }
        }
    }
}
