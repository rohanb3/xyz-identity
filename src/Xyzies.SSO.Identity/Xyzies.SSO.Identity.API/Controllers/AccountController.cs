using System.IO;
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

        public AccountController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
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
        public IActionResult Login([FromQuery]AuthorizeModel authorizeModel)
        {
        //http://localhost:8081/api/account/.well-known/openid-configuration
            return Redirect("https://ardasdev.b2clogin.com/ardasdev.onmicrosoft.com/oauth2/authresp");
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout(string email, string password)
        {
            return Ok();
        }
    }
}