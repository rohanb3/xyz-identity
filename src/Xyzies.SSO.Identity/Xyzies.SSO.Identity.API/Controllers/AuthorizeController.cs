using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Models.User;
using static Xyzies.SSO.Identity.Data.Helpers.Consts;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthService _authorizationService;

        public AuthorizeController(IAuthService authorizationService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        }

        /// <summary>
        /// Authorizes user with passed credentials
        /// </summary>
        /// <param name="grant_type">Grant type for authorization, supported types are 'password' and 'refresh_token'</param>
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <param name="scope">scope what user will use to work with. Example - xyzies.authorization.reviews.admin</param>
        /// <param name="refresh_token">Refresh token</param>
        /// <returns></returns>
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        public async Task<IActionResult> Token(string grant_type, UserAuthorizeOptions credentials)
        {
            try
            {
                var passwordResult = await _authorizationService.AuthorizeAsync(credentials);
                return Ok(passwordResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(AccessViolationException ex)
            {
                return Forbid(ex.Message);
            }
        }

        /// <summary>
        /// Return refreshed access_token
        /// </summary>
        /// <param name="refresh_token">Refresh token</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        public async Task<IActionResult> Refresh(string refresh_token)
        {
            var refreshResult = await _authorizationService.RefreshAsync(refresh_token);
            return Ok(refreshResult);
        }
}