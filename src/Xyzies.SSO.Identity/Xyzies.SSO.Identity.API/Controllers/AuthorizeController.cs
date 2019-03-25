using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Exceptions;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/authorize")]
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
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <param name="scope">scope what user will use to work with. Example - xyzies.authorization.reviews.admin</param>
        /// <returns>Access token with additional info</returns>
        [HttpPost("token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ContentResult))]
        public async Task<IActionResult> Token(UserAuthorizeOptions credentials)
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
            catch (AccessException ex)
            {
                return new ContentResult { StatusCode = 403, Content = ex.Message, ContentType = "application/json" };
            }
        }

        /// <summary>
        /// Return refreshed access_token
        /// </summary>
        /// <param name="refresh_token">Refresh token</param>
        /// <returns>Refreshed access_token</returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ContentResult))]
        public async Task<IActionResult> Refresh(UserRefreshOptions refresh)
        {
            try
            {
                var refreshResult = await _authorizationService.RefreshAsync(refresh);
                return Ok(refreshResult);
            }
            catch (AccessException ex)
            {
                return new ContentResult { StatusCode = 403, Content = ex.Message, ContentType = "application/json" };
            }
        }
    }
}