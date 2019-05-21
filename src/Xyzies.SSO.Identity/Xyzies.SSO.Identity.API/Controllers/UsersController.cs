using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.Services.Exceptions;
using Xyzies.SSO.Identity.Data.Core;
using Xyzies.SSO.Identity.Data.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Xyzies.SSO.Identity.API.Controllers
{
    /// <summary>
    /// AAD Users Endpoints
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Ctor with dependencies
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUserService userService)
        {
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Returns collection of users
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        public async Task<IActionResult> Get([FromQuery] UserFilteringParams filter, [FromQuery]UserSortingParameters sorting)
        {
            try
            {
                var currentUser = new UserIdentityParams
                {
                    Id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.UserIdPropertyName)?.Value,
                    Role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.RoleClaimType)?.Value,
                    CompanyId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.CompanyIdClaimType)?.Value
                };
                var users = await _userService.GetAllUsersAsync(currentUser, filter, sorting);

                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }

        /// <summary>
        /// Returns user for trusted service
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpGet]
        [Route("{token}/trusted/{objectId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        public async Task<IActionResult> GetAllUsersForTrustedService(string token, string objectId)
        {
            try
            {
                if (token != Consts.Security.StaticToken)
                {
                    return new ContentResult { StatusCode = 403 };
                }

                var currentUser = new UserIdentityParams
                {
                    Role = Consts.Roles.OperationsAdmin
                };

                var user = await _userService.GetUserByIdAsync(objectId, currentUser);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                        new KeyValuePair<string, string[]>(ex.ParamName ?? "Unknown" , new string[] { ex.Message })
                    })));
            }
            catch (KeyNotFoundException)
            {
                return NotFound("User with current id not found");
            }
        }

        /// <summary>
        /// Returns collection of users for trusted service
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        [HttpGet]
        [Route("{token}/trusted")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(IEnumerable<Profile>))]
        public async Task<IActionResult> GetAllUsersForTrustedService(string token)
        {
            try
            {
                if (token != Consts.Security.StaticToken)
                {
                    return new ContentResult { StatusCode = 403 };
                }

                var currentUser = new UserIdentityParams
                {
                    Role = Consts.Roles.OperationsAdmin
                };
                var users = await _userService.GetAllUsersAsync(currentUser);

                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                        new KeyValuePair<string, string[]>(ex.ParamName ?? "Unknown" , new string[] { ex.Message })
                    })));
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }

        /// <summary>
        /// Returns collection of users for trusted service by passed filters. POST method required to pass large collections inside filters
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        [HttpPost]
        [Route("{token}/trusted/filtered")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        public async Task<IActionResult> GetFilteredUsersForTrustedService(string token, [FromBody] UserFilteringParams filters)
        {
            try
            {
                if (token != Consts.Security.StaticToken)
                {
                    return new ContentResult { StatusCode = 403 };
                }

                var currentUser = new UserIdentityParams
                {
                    Role = Consts.Roles.OperationsAdmin
                };
                var users = await _userService.GetAllUsersAsync(currentUser, filters);

                return Ok(users);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                        new KeyValuePair<string, string[]>(ex.ParamName ?? "Unknown" , new string[] { ex.Message })
                    })));
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }


        /// <summary>
        /// Returns total count of users
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpGet]
        [Route("total")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        public IActionResult GetTotalInCompanies([FromQuery] List<string> companyIds, [FromQuery] UserSortingParameters sorting, [FromQuery] LazyLoadParameters lazyParameters)
        {
            try
            {
                var userRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.RoleClaimType)?.Value;
                if (!string.IsNullOrEmpty(userRole) && !Consts.Roles.GlobalAdmins.Contains(userRole.ToLower()))
                {
                    return new ContentResult { StatusCode = 403 };
                }

                var usersCount = _userService.GetUsersCountInCompanies(companyIds, sorting, lazyParameters);

                return Ok(usersCount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Returns total count of users
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpGet]
        [Route("total/{token}/trusted")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Profile>))]
        public IActionResult GetAllForTrustedService(string token)
        {
            try
            {
                var usersCount = _userService.GetUsersCountInCompanies();

                return Ok(usersCount);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get user by his Cable Portal id
        /// </summary>
        /// <param name="id">Cable Portal id</param>
        /// <param name="token">Static token</param>
        /// <returns>User with passed identifier, or not found response</returns>
        /// <response code="200">If user fetched successfully</response>
        /// <response code="403">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [AllowAnonymous]
        [HttpGet("{token}/cp/{id}", Name = "UserByCPId")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Profile))]
        public async Task<IActionResult> Get(string token, int id)
        {
            try
            {
                if (token != Consts.Security.StaticToken)
                {
                    return new ContentResult { StatusCode = 403 };
                }

                var user = await _userService.GetUserBy(u => u.CPUserId == id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }



        /// <summary>
        /// Get user by his id, objectId or userPrincipalName
        /// </summary>
        /// <param name="id">Azure AD B2C user uniq identifier or integer. Can be objectId of userPrincipalName</param>
        /// <returns>User with passed identifier, or not found response</returns>
        /// <response code="200">If user fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [HttpGet("{id}", Name = "User")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Profile))]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var currentUser = new UserIdentityParams
                {
                    Id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.UserIdPropertyName)?.Value,
                    Role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.RoleClaimType)?.Value,
                    CompanyId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.CompanyIdClaimType)?.Value
                };
                var user = await _userService.GetUserByIdAsync(id, currentUser);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }

        /// <summary>
        /// Get user by his objectId from token
        /// </summary>
        /// <returns>User with passed identifier, or not found response</returns>
        /// <response code="200">If user fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProfileSecure))]
        [HttpGet("profile")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == Consts.UserIdPropertyName)?.Value;

                return Ok(await _userService.GetOwnProfile(userId));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }

        /// <summary>
        /// Delete user by his objectId or userPrincipalName
        /// </summary>
        /// <param name="id">Azure AD B2C user uniq identifier. Can be objectId of userPrincipalName</param>
        /// <returns>User with passed identifier, or not found response</returns>
        /// <response code="204">If user deleted successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [HttpDelete("{id}", Name = "User")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteUserByIdAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AccessException)
            {
                return new ContentResult { StatusCode = 403 };
            }
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="userCreatable">User DTO to create</param>
        /// <returns>URL to newly created user</returns>
        /// <response code="201">If user fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Profile))]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] [Required] ProfileCreatable userCreatable)
        {
            try
            {
                string token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ').LastOrDefault();
                var userToResponse = await _userService.CreateUserAsync(userCreatable, token);
                return Ok(userToResponse);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>(new List<KeyValuePair<string, string[]>> {
                            new KeyValuePair<string, string[]>(ex.ParamName, new string[] { ex.Message })
                        })));
            }
        }

        /// <summary>
        /// Update user properties by objectId or userPrincipalName
        /// </summary>
        /// <param name="objectId">objectId or userPrincipalName of Azure AD B2C User</param>
        /// <param name="userToUpdate">User DTO to update</param>
        /// <returns>URL to updated user</returns>
        /// <response code="201">If user updated successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [HttpPatch("{objectId}")]
        public async Task<IActionResult> Patch(string objectId, [FromBody] [Required] BaseProfile userToUpdate)
        {
            try
            {
                await _userService.UpdateUserByIdAsync(objectId, userToUpdate);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update a user photo thumbnail
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatarModel"></param>
        /// <returns></returns>
        /// <response code="204">If avatar updated successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="400">If avatar not updated</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{userId}/avatar")]
        public async Task<IActionResult> UploadAvatar([FromRoute][Required] string userId, [FromForm] [Required] AvatarModel avatarModel)
        {
            try
            {
                await _userService.UploadAvatar(userId, avatarModel);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete a user photo thumbnail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">If avatar updated successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="400">If avatar not deleted</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{userId}/avatar")]
        public async Task<IActionResult> DeleteAvatar([FromRoute][Required] string userId)
        {
            try
            {
                await _userService.DeleteAvatar(userId);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get a user photo thumbnail
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">If avatar updated successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If avatar not found</response>
        // TODO: Fix 'image/*' to valid type
        //[Produces("image/*")] 
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK /* 200 */)]
        [ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [HttpGet("{userId}/avatar")]
        public async Task<IActionResult> GetAvatar([FromRoute, Required] string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var avatarFile = await _userService.GetAvatar(userId);
                if (avatarFile == null)
                {
                    return NotFound();
                }

                return File(avatarFile.FileBytes, avatarFile.ContentType);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}