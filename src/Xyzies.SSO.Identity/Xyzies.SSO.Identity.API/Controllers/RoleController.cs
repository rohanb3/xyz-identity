using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xyzies.SSO.Identity.Services.Models.Permissions;
using Xyzies.SSO.Identity.Services.Service.Permission;
using Xyzies.SSO.Identity.Services.Service.Roles;

namespace Xyzies.SSO.Identity.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/role")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService = null;
        private readonly ILogger<RoleController> _logger = null;
        private readonly IPermissionService _permissionService = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="roleRepository"></param>
        public RoleController(ILogger<RoleController> logger,
            IRoleService roleRepository,
            IPermissionService permissionService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _roleService = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _permissionService = permissionService ?? throw new ArgumentNullException(nameof(permissionService));
        }

        [HttpHead]
        public async Task<IActionResult> HasPermission([FromQuery] string[] scope, [FromQuery] string role)
        {
            await _permissionService.CheckPermissionExpiration();
            var hasPermission = _permissionService.CheckPermission(role, scope);
            if (!hasPermission)
            {
                return new ContentResult { StatusCode = 403 };
            }
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }
    }
}