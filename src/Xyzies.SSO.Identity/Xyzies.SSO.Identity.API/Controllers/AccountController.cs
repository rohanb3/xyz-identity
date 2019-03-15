using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IActionResult> Login([FromQuery]AuthorizeModel authorizeModel)
        {
            string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var securityKey = new Microsoft
               .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                             (securityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);
            var payload = new JwtPayload
           {
               { "exp ", "1552659649"},
               { "nbf", "1552656049"},
               { "ver", "1.0"},
               { "iss", "https://ardasdev.b2clogin.com/f1018aa9-8f54-4999-8f24-6e55b4695bb0/v2.0/"},
               { "sub", "f790a93a-b668-4827-9ed5-41e2d3bd3fed"},
               { "aud", "eafbe588-0582-4880-9635-6edc7cc8d798"},
               { "nonce", "b4qbF5i62UtmycZrxN/9RA=="},
               { "iat", "1552656049"},
               { "auth_time", "1552656049"},
               { "oid", "f790a93a-b668-4827-9ed5-41e2d3bd3fed"},
               { "extension_Group", "SuperAdmin"},
               { "tfp", "B2C_1_SiInUp"}
           };
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            try
            {
                return Challenge(
                    new AuthenticationProperties { RedirectUri = "google.com" },
                    OpenIdConnectDefaults.AuthenticationScheme);
            }
            catch(Exception e)
            {
                var a = e;
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout(string email, string password)
        {
            return Ok();
        }
    }
}