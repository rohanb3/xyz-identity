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
            return Ok("eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJleHAiOjE1NTIzODM4ODcsIm5iZiI6MTU1MjI5NzQ4NywidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly9hcmRhc2Rldi5iMmNsb2dpbi5jb20vZjEwMThhYTktOGY1NC00OTk5LThmMjQtNmU1NWI0Njk1YmIwL3YyLjAvIiwic3ViIjoiNzVjZjBiM2QtOGNlZC00ZDJlLWI1NDgtOGM0MGZhYmU3M2UyIiwiYXVkIjoiZWFmYmU1ODgtMDU4Mi00ODgwLTk2MzUtNmVkYzdjYzhkNzk4Iiwibm9uY2UiOiJkZWZhdWx0Tm9uY2UiLCJpYXQiOjE1NTIyOTc0ODcsImF1dGhfdGltZSI6MTU1MjI5NzQ4Nywib2lkIjoiNzVjZjBiM2QtOGNlZC00ZDJlLWI1NDgtOGM0MGZhYmU3M2UyIiwiZXh0ZW5zaW9uX0dyb3VwIjoiU2FsZXNSZXAiLCJ0ZnAiOiJCMkNfMV9TaUluIn0.OR6Ja8OwB5HCw-rzK8tdfUWHRCfbYa-Hf249KFudG_YaRRFl-c9t1iraFLnsChgTpF0mYhHWnSubm5RrouZTFsWuMFyaFMtAEFHJutKjM-frEttZyQuW5Ei_7xf5nBMTsTvsuP6BN1rCDD8yPKyKqqlVZ8BOEhxd9BQsb1CsnVSQOlHkMZ6i8ZBTzKtOxBcGv26WPAJuam3C8xRBJ_7DZV48nVtco4I1WByVRL48dhrB57T2vqjHfsRaodHnGixZZIQPf08J69qFl2jBF3eP8_Xt_QdhXzmukuX9M3EX5YblJFBquJYXFHMoWy7iq8RuluHqbZX7FmgD08nInpvIaw");
        }

        [HttpGet]
        [Route("info")]
        public IActionResult GetInfo(string email, string password)
        {
            return Ok(new CpUser { Email = "blabla@gmail.com", Id = 1, Name = "Name" });
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

        [HttpPost]
        [Route("authorize")]
        public IActionResult Login(string email, string password)
        {
        http://localhost:8081/api/account/.well-known/openid-configuration
            return Redirect("https://google.com");
        }

        [HttpPost]
        [Route("logout")]
        public IActionResult Logout(string email, string password)
        {
            return Ok();
        }
    }
}