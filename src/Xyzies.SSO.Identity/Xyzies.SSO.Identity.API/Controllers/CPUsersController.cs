using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Services.Service;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/cpusers")]
    [ApiController]
    //[Authorize]
    public class CpUsersController : ControllerBase
    {
        private readonly ICpUsersService _users;

        public CpUsersController(ICpUsersService users)
        {
            _users = users;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery]LazyLoadParameters lazyLoad)
        {
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.RoleClaimType)?.Value;
            var companyId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.CompanyIdPropertyName)?.Value;

            var users = await _users.GetAllCpUsers(role, companyId, lazyLoad);
            if (users == null)
            {
                return new ContentResult { StatusCode = 403 };
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var userId = int.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.UserIdPropertyName)?.Value);
            var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.RoleClaimType)?.Value;
            var companyId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.CompanyIdPropertyName)?.Value;

            var user = await _users.GetUserById(id, userId, role, companyId);
            if (user == null)
            {
                return new ContentResult { StatusCode = 403 };
            }
            return Ok(user);
        }
    }
}