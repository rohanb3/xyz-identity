using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xyzies.SSO.Identity.Services.Service.Permission;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Data.Helpers;
using System.Net;

namespace Xyzies.SSO.Identity.API.Filters
{
    public class AccessFilter : Attribute, IAsyncActionFilter
    {
        public string Scopes { get; set; }
        public IPermissionService _permissionService;
        public AccessFilter(string scopes)
        {
            Scopes = scopes;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();
            var role = context.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == Consts.RoleClaimType)?.Value;
            var scopes = Scopes.Split(',');
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
