using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xyzies.SSO.Identity.Services.Service;
using Xyzies.SSO.Identity.UserMigration.Services;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/location")]
    [ApiController]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocaltionService _localtionService;
        private readonly IMigrationService _migration;

        public LocationController(ILocaltionService localtionService, IMigrationService migration)
        {
            _localtionService = localtionService;
            _migration = migration;
        }

        [HttpGet]
        [Route("{stateName}/city")]
        public async Task<IActionResult> GetAll(string stateName)
        {
            var cities = await _localtionService.GetAllCities(stateName);
            return Ok(cities);
        }

        [HttpGet]
        [Route("city")]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _localtionService.GetAllCities();
            return Ok(cities);
        }

        [HttpGet]
        [Route("state")]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _localtionService.GetAllStates();
            return Ok(states);
        }
    }
}