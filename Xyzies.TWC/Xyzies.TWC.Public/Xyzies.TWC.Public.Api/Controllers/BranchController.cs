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
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace Xyzies.TWC.Public.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api")]
    [ApiController]
    public class BranchController : Controller
    {
        private readonly ILogger<BranchController> _logger = null;
        private readonly IBranchRepository _branchRepository = null;
        private readonly IBranchManager _branchManager = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="branchRepository"></param>
        /// <param name="branchManager"></param>
        public BranchController(ILogger<BranchController> logger,
            IBranchRepository branchRepository,
            IBranchManager branchManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
            _branchManager = branchManager ?? throw new ArgumentNullException(nameof(branchManager));
        }

        /// <summary>
        /// GET api/branches
        /// </summary>
        /// <returns></returns>
        [HttpGet("branch", Name = "GetListBranches")]
        [ProducesResponseType(typeof(PagingResult<BranchModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NoContent /* 404 */)]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
        public async Task<IActionResult> Get(
            [FromQuery] Filter filterModel,
            [FromQuery] Sortable sortable,
            [FromQuery] Paginable paginable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _branchManager.GetBranches(filterModel, sortable, paginable);
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
        [HttpGet("branch/{id}", Name = "GetBranchDetails")]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound /* 404 */)]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
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
        /// Get all branches related to specify company
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="filterModel"></param>
        /// <param name="sortable"></param>
        /// <param name="paginable"></param>
        /// <returns></returns>
        [HttpGet("company/{company_id}/branch")]
        [ProducesResponseType(typeof(IEnumerable<UploadBranchModel>), 200)]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(NotFoundResult), 404)] // By handling an exception middleware
        [SwaggerOperation(Tags = new[] { "Company API" })]
        public async Task<IActionResult> GetBranchOfCompany(
            [FromRoute] int companyId,
            [FromQuery] Filter filterModel,
            [FromQuery] Sortable sortable,
            [FromQuery] Paginable paginable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _branchManager.GetBranchesByCompany(companyId, filterModel, sortable, paginable);

            if (result.Total.Equals(0))
            {
                return NoContent();
            }

            return Ok(result);

        }

        /// <summary>
        /// POST api/branches
        /// </summary>
        /// <param name="branchModel"></param>
        /// <returns></returns>
        [HttpPost("branch", Name = "CreateBranch")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Created /* 201 */)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
        public IActionResult Post([FromBody] UploadBranchModel branchModel)
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
        [HttpPut("branch/{id}", Name = "UpdateBranch")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK /* 200 */)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound /* 404 */)]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
        public IActionResult Put([FromRoute]int id, [FromBody] UploadBranchModel branchModel)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        [HttpPatch("branch/{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest /* 400 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Unauthorized /* 401 */)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound /* 404 */)]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
        public IActionResult Patch([FromRoute]int id, [FromQuery] bool isEnabled)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Refactoring
            var entityState = _branchRepository.SetActivationState(id, isEnabled);

            return Ok(entityState);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "Branch API" })]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        // TODO: Dispose
        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: Disposing
                _branchRepository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
