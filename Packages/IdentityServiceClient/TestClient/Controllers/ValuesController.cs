﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServiceClient.Service;
using Microsoft.AspNetCore.Mvc;

namespace TestClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IIdentityManager _identityManager;
        public ValuesController(IIdentityManager identityManager)
        {
            _identityManager = identityManager;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _identityManager.GetAllUsersAsync();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
