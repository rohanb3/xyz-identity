using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.Services.Models.User;
using Xyzies.SSO.Identity.API.Filters;
using Xyzies.SSO.Identity.Data.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Returns collection of users from Active Directory
        /// </summary>
        /// <returns>Collection of users</returns>
        /// <response code="200">If users fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpGet]
        [AccessFilter(Consts.Scopes.Full)]
        public async Task<IActionResult> Get([FromQuery] UserFilteringParams filter)
        {
            var users = await _userService.GetAllUsersAsync(filter);
            return Ok(users);
        }

        /// <summary>
        /// Get user by his objectId or userPrincipalName
        /// </summary>
        /// <param name="id">Azure AD B2C user uniq identifier. Can be objectId of userPrincipalName</param>
        /// <returns>User with passed identifier, or not found response</returns>
        /// <response code="200">If user fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        /// <response code="404">If user was not found</response>
        [HttpGet("{id}", Name = "User")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
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
            catch (AccessViolationException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="userCreatable">User DTO to create</param>
        /// <returns>URL to newly created user</returns>
        /// <response code="201">If user fetched successfully</response>
        /// <response code="401">If authorization token is invalid</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] [Required] ProfileCreatable userCreatable)
        {
            try
            {
                var userToResponse = await _userService.CreateUserAsync(userCreatable);
                return Ok(userToResponse);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
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
    }
}