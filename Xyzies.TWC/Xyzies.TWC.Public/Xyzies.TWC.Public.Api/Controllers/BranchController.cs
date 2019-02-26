﻿using System;
using Mapster;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xyzies.TWC.Public.Api.Controllers.Http.Extentions;
using Xyzies.TWC.Public.Api.Managers.Interfaces;
using Xyzies.TWC.Public.Api.Models;
using Xyzies.TWC.Public.Data.Entities;
using Xyzies.TWC.Public.Data.Repositories.Interfaces;

namespace Xyzies.TWC.Public.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/branch")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchManager _branchRequestManager = null;
        private readonly IBranchRepository _branchRepository = null;
        private readonly ILogger<BranchController> _logger = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="branchRepository"></param>
        /// <param name="branchRequestManager"></param>
        public BranchController(ILogger<BranchController> logger,
            IBranchRepository branchRepository, IBranchManager branchRequestManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
            _branchRequestManager = branchRequestManager;
        }

        /// <summary>
        /// GET api/branches
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetListBranches")]
        [ProducesResponseType(typeof(PagingResult<BranchModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent /* 404 */)]
        public async Task<IActionResult> Get(
            [FromQuery] BranchFilter filterModel,
            [FromQuery] Sortable sortable,
            [FromQuery] Paginable paginable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _branchRequestManager.GetBranches(filterModel, sortable, paginable);
            if (!result.Data.Any())
            {
                return NoContent();
            }

            return Ok(result);
        }

        /// <summary>
        /// GET api/branch/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetBranchDetails")]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound /* 404 */)]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Branch branchDetail = null;
            try
            {
                branchDetail = await _branchRepository.GetAsync(id);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            if (branchDetail == null)
            {
                return NotFound();
            }
            var branchDetailModel = branchDetail.Adapt<BranchModel>();

            return Ok(branchDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchModel"></param>
        /// <returns></returns>
        [HttpPost(Name = "CreateBranch")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created /* 201 */)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        public IActionResult Post([FromBody] BranchModel branchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var branchEntity = branchModel.Adapt<Branch>();

            int branchId;
            try
            {
                branchId = _branchRepository.Add(branchEntity);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(branchId);
        }

        /// <summary>
        /// api/branch/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="branchModel"></param>
        [HttpPut("{id}", Name = "UpdateBranch")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK /* 200 */)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound /* 404 */)]
        public IActionResult Put(int id, [FromBody] BranchModel branchModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = false;
            var branchEntity = branchModel.Adapt<Branch>();
            branchEntity.Id = id;

            try
            {
                result = _branchRepository.Update(branchEntity);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            if (result.Equals(false))
            {
                return NotFound($"Update failed. Branch with id={id} not found");
            }

            return Ok(result);
        }

        // DELETE api/branch/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
