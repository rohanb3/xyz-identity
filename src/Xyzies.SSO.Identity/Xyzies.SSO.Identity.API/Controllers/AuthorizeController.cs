using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.API.Models;
using Xyzies.SSO.Identity.Services.Service.ResetPassword;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthService _authorizationService;
        private readonly IResetPasswordService _resetPasswordService;

        public AuthorizeController(IAuthService authorizationService, IResetPasswordService resetPasswordService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _resetPasswordService = resetPasswordService ?? throw new ArgumentNullException(nameof(resetPasswordService));
        }

#pragma warning disable CS1572 // XML comment has a param tag, but there is no parameter by that name
        /// <summary>
        /// Authorizes user with passed credentials
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">User password</param>
        /// <param name="scope">scope what user will use to work with. Example - xyzies.authorization.reviews.admin</param>
        /// <returns>Access token with additional info</returns>
        [HttpPost("token")]
#pragma warning restore CS1572 // XML comment has a param tag, but there is no parameter by that name
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ContentResult))]
#pragma warning disable CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        public async Task<IActionResult> Token(UserAuthorizeOptions credentials)
#pragma warning restore CS1573 // Parameter has no matching param tag in the XML comment (but other parameters do)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _authorizationService.AuthorizeAsync(credentials));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessException ex)
            {
                return new ContentResult
                {
                    StatusCode = 403,
                    Content = ex.Message,
                    ContentType = "application/json"
                };
            }
        }

        /// <summary>
        /// Return refreshed access_token
        /// </summary>
        /// <returns>Refreshed access_token</returns>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ContentResult))]
        public async Task<IActionResult> Refresh(UserRefreshOptions refresh)
        {
            try
            {
                return Ok(await _authorizationService.RefreshAsync(refresh));
            }
            catch (AccessException ex)
            {
                return new ContentResult
                {
                    StatusCode = 403,
                    Content = ex.Message,
                    ContentType = "application/json"
                };
            }
        }

        /// <summary>
        /// Sends verification code to current email, creates row in DB with reset password request data
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpPost("request-verification-code")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        public async Task<IActionResult> SendResetLink([FromBody] RequestVerificationCodeModel options)
        {
            await _resetPasswordService.SendConfirmationCodeAsync(options.Email);

            return Ok();
        }

        /// <summary>
        /// Verifys confirmation code for passed email
        /// </summary>
        /// <param name="options"></param>
        /// <returns>If success, returns reset token to reset password for konfirmed user</returns>
        [HttpPost("verify-code")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResetTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(NotFoundObjectResult))]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeModel options)
        {
            try
            {
                var result = await _resetPasswordService.ValidateConfirmationCodeAsync(options.Email, options.Code);
                return Ok(new ResetTokenResponse { ResetToken = result });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    errors = new
                    {
                        Code = new[] { "Code is not valid" }
                    },
                    status = StatusCodes.Status400BadRequest
                });
            }
        }

        /// <summary>
        /// Resets password by resetToken
        /// </summary>
        /// <param name="options"></param>
        /// <returns>If success, resets password for konfirmed user</returns>
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult))]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordModel options)
        {
            try
            {
                await _resetPasswordService.ResetPassword(options.ResetToken, options.Password);

                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return BadRequest();
            }
        }
    }
}