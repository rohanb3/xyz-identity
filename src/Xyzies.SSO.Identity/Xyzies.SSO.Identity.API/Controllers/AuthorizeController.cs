using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xyzies.SSO.Identity.API.Models;
using Xyzies.SSO.Identity.Data.Helpers;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Service.ResetPassword;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/authorize")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthService _authorizationService;
        private readonly IResetPasswordService _resetPasswordService;
        private readonly ILogger _logger;

        public AuthorizeController(IAuthService authorizationService, IResetPasswordService resetPasswordService, ILogger<AuthorizeController> logger)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _resetPasswordService = resetPasswordService ?? throw new ArgumentNullException(nameof(resetPasswordService));
            _logger = logger ?? throw new ArgumentNullException(nameof(resetPasswordService));
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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ContentResult))]
        public async Task<IActionResult> Token(UserAuthorizeOptions credentials)
        {
            try
            {
                _logger.LogError("Test logs. Login by {username},{scope}", credentials.Username, credentials.Scope);
                return Ok(await _authorizationService.AuthorizeAsync(credentials));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == Consts.ErrorReponses.UserDoesNotExits)
                {
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>("Email", new string[] { "This email is not registered in the system. Please, check and try again" })
                        })));
                }

                throw ex;
            }
            catch (AccessException ex)
            {
                if (ex.Message.Contains(Consts.ErrorReponses.AzureLoginError))
                {
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>("Password", new string[] { "Password Incorrect" })
                        })));
                }

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
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
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
        public async Task<IActionResult> SendResetCode([FromBody] RequestVerificationCodeModel options)
        {
            try
            {
                await _resetPasswordService.SendConfirmationCodeAsync(options.Email);

                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.Message == Consts.ErrorReponses.UserDoesNotExits)
                {
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>("Email", new string[] { "User does not exist" })
                        })));
                }

                throw ex;
            }
        }

        /// <summary>
        /// Verifys confirmation code for passed email
        /// </summary>
        /// <param name="options"></param>
        /// <returns>If success, returns reset token to reset password for konfirmed user</returns>
        [HttpPost("verify-code")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResetTokenResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
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
                if (ex.Message == Consts.ErrorReponses.CodeIsNotValid)
                {
                    return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>("Code", new string[] { "Code is not valid" })
                        })));
                }

                throw ex;
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
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>("ResetToken", new string[] { "Token is not valid" })
                        })));
            }
        }
    }
}