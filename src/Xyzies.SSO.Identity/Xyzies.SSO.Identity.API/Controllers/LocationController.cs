﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Xyzies.SSO.Identity.Data.Entity;
using Xyzies.SSO.Identity.Services.Service;

namespace Xyzies.SSO.Identity.API.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocaltionService _localtionService;

        public LocationController(ILocaltionService localtionService)
        {
            _localtionService = localtionService;
        }

        /// <summary>
        /// Get cities for current state
        /// </summary>
        /// <param name="stateName">Name of state</param>
        /// <returns>Collection of cities for passed state</returns>
        [HttpGet]
        [Route("{stateName}/city")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        public async Task<IActionResult> GetAll(string stateName)
        {
            var cities = await _localtionService.GetAllCities(stateName);
            return Ok(cities);
        }

        /// <summary>
        /// Get cities
        /// </summary>
        /// <returns>Collection of cities</returns>
        [HttpGet]
        [Route("city")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        public async Task<IActionResult> GetAllCities()
        {
            var cities = await _localtionService.GetAllCities();
            return Ok(cities);
        }

        /// <summary>
        /// Get cities
        /// </summary>
        /// <returns>Collection of cities</returns>
        [HttpGet]
        [Route("city/ids")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<City>))]
        public async Task<IActionResult> GetCitiesByIds([FromQuery] List<Guid> ids)
        {
            var cities = await _localtionService.GetAllCities(ids);
            return Ok(cities);
        }

        /// <summary>
        /// Get states
        /// </summary>
        /// <returns>Collection of states</returns>
        [HttpGet]
        [Route("state")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<State>))]
        public async Task<IActionResult> GetAllStates()
        {
            var states = await _localtionService.GetAllStates();
            return Ok(states);
        }

        /// <summary>
        /// Get states
        /// </summary>
        /// <returns>Collection of states</returns>
        [HttpGet]
        [Route("state/ids")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<State>))]
        public async Task<IActionResult> GetStatesByIds([FromQuery] List<Guid> ids)
        {
            var states = await _localtionService.GetAllStates(ids);
            return Ok(states);
        }
    }
}
