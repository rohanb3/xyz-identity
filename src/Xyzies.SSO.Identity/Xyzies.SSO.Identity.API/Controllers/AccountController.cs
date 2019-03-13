using System.IO;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Services.Models.User;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IHostingEnvironment _appEnvironment;
        private readonly IEventService _events;
        private readonly IIdentityServerInteractionService _interaction;

        public AccountController(IHostingEnvironment appEnvironment, IIdentityServerInteractionService interaction, IEventService events)
        {
            _appEnvironment = appEnvironment;
            _interaction = interaction;
            _events = events;
        }

        [HttpGet]
        [Route("token")]
        public IActionResult GetToken(string email, string password)
        {
            return Redirect("https://google.com");
        }

        [HttpGet]
        [Route("info")]
        public IActionResult GetInfo(string email, string password)
        {
            return Redirect("https://translate.google.com/");
        }

        [HttpGet]
        [Route(".well-known/openid-configuration")]
        public IActionResult GetConfig()
        {
            var path = Path.Combine(_appEnvironment.ContentRootPath, "OpenIdConfiguration/openid-configuration");
            return new PhysicalFileResult(path, "application/json");
        }

        [HttpGet]
        [Route(".well-known/openid-configuration/jwks")]
        public IActionResult GetJwks()
        {
            var path = Path.Combine(_appEnvironment.ContentRootPath, "OpenIdConfiguration/jwks");
            return new PhysicalFileResult(path, "application/json");
        }

        [HttpGet]
        [Route("authorize")]
        public async Task<IActionResult> Login([FromQuery]AuthorizeModel authorizeModel)
        {
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var id_token = result.Properties?.GetTokenValue("id_token");
            await _events.RaiseAsync(new UserLoginSuccessEvent("SuperAdmin", "1", "Super"));
            AuthenticationProperties props = null;
            await HttpContext.SignInAsync("1", "Super", props);
            return Redirect(authorizeModel.redirect_uri + "&id_token=" + id_token);
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout(string email, string password)
        {
            return Ok();
        }
    }
}