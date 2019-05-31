using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.UserMigration.Services.Migrations;

namespace Xyzies.SSO.Identity.API.Controllers
{
    /// <summary>
    /// Controller for user migrations between CP and B2C
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/migrations")]
    public class MigrationsController : ControllerBase
    {
        private readonly IMigrationService _migrationService;

        /// <summary>
        /// Constructor with dependecies
        /// </summary>
        /// <param name="migrationService"></param>
        public MigrationsController(IMigrationService migrationService)
        {
            _migrationService = migrationService ??
                throw new ArgumentNullException(nameof(migrationService));
        }

        /// <summary>
        /// Migrate users from Azure AD B2C to Cable Portal DB
        /// </summary>
        [HttpGet("users/azure-to-cp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> MigrateAzureToCp()
        {
            await _migrationService.MigrateAzureToCPAsync();
            return Ok();
        }

        /// <summary>
        /// Migrate users from CP base to Azure
        /// </summary>
        /// <param name="limit">Limit of users from CP base</param>
        /// <param name="offset">Offset for users from CP base</param>
        /// <param name="emails">Specify users by their mail</param>
        /// <returns></returns>
        [HttpGet("users/cp-to-azure")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Migrate([FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] string[] emails)
        {
            await _migrationService.MigrateCPToAzureAsync(new UserMigration.Models.MigrationOptions { Limit = limit, Offset = offset, Emails = emails });
            return Ok();
        }

        [HttpGet("fill-roles")]
        public async Task<IActionResult> FillRoles()
        {
            await _migrationService.ChangeRoleName();

            return Ok();
        }

        /// <summary>
        /// Fill SuperAdmins with default company branches
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="emails"></param>
        /// <returns></returns>
        [HttpGet("fill-sa-branches")]
        public async Task<IActionResult> FillBranches([FromQuery] int? limit, [FromQuery] int? offset, [FromQuery] string[] emails)
        {
            string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ').LastOrDefault();
            await _migrationService.FillSuperAdminsWithDefaultBranches(token, new UserMigration.Models.MigrationOptions { Limit = limit, Offset = offset, Emails = emails });

            return Ok();
        }
    }
}
