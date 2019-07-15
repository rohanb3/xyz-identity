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

namespace IdentityServiceClient.Filters
{
    public class AccessFilter : Attribute, IAsyncActionFilter
    {
        public string[] Scopes { get; set; }
        public IIdentityManager _manager;

        public AccessFilter(params string[] scopes)
        {
            Scopes = scopes;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _manager = context.HttpContext.RequestServices.GetService<IIdentityManager>();
            var bearerToken = (context.HttpContext.Request.Headers[Const.Auth.AuthHeader]).ToString();
            var hasPermissionResultList = new List<bool>();
            if (string.IsNullOrEmpty(bearerToken))
            {
                context.Result = new ContentResult { StatusCode = 401 };
            }
            else
            {
                bearerToken = bearerToken.Substring(Const.Auth.BearerToken.Length);
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(bearerToken);
                var role = tokenS.Claims.FirstOrDefault(claim => claim.Type == Const.Permissions.RoleClaimType)?.Value;
                foreach (var scope in Scopes)
                {
                    var scopesForOneRole = scope.Split(',');
                    hasPermissionResultList.Add(await _manager.HasPermission(role, scopesForOneRole));
                }
                if (hasPermissionResultList.All(x => !x))
                {
                    context.Result = new ContentResult { StatusCode = 403, Content = $"You don't have any permission" };
                }
                else
                {
                    await next();
                }
            }
        }
    }
}
