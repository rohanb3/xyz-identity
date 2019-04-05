using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Xyzies.SSO.Identity.Services.Service.Permission;
using Xyzies.SSO.Identity.Data.Helpers;

namespace Xyzies.SSO.Identity.API.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class AccessFilter : Attribute, IAsyncActionFilter
    {
        private IPermissionService _permissionService = null;

        /// <summary>
        /// 
        /// </summary>
        public string Scopes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopes"></param>
        public AccessFilter(string scopes)
        {
            Scopes = scopes;
        }

        /// <inheritdoc />
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>() ??
                throw new InvalidOperationException("Missing object of IPermissionService");

            string role = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == Consts.RoleClaimType)?.Value;
            string[]  scopes = Scopes.Split(',');

            await _permissionService.CheckPermissionExpiration();

            var hasPermission = _permissionService.CheckPermission(role, scopes);
            if (!hasPermission)
            {
                context.Result = new ContentResult { StatusCode = 403 };
            }

            await next();
        }
    }
}
