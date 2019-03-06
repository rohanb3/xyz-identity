using IdentityServiceClient.Service;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServiceClient.Filters
{
    public class AccessFilter : Attribute, IAsyncActionFilter
    {
        public string Scopes { get; set; }
        public IIdentityManager _manager;
        public AccessFilter(string scopes)
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
            if (!string.IsNullOrEmpty(bearerToken))
            {
                bearerToken = bearerToken.Substring(Const.Auth.BearerToken.Length);
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadJwtToken(bearerToken);
                var role = tokenS.Claims.FirstOrDefault(claim => claim.Type == Const.Permissions.RoleClaimType)?.Value;
                var scopes = Scopes.Split(',');
                var hashPermission = await _manager.HasPermission(role, scopes);
                if (!hashPermission)
                {
                    context.Result = new ContentResult { StatusCode = 403 };
                }
            }
            else
            {
                context.Result = new ContentResult { StatusCode = 403 };
            }

            await next();
        }
    }
}
