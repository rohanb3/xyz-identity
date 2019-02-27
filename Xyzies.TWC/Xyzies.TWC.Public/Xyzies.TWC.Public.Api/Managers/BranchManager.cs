﻿using Mapster;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xyzies.TWC.Public.Api.Controllers;
using Xyzies.TWC.Public.Api.Controllers.Http.Extentions;
using Xyzies.TWC.Public.Api.Managers.Interfaces;
using Xyzies.TWC.Public.Api.Models;
using Xyzies.TWC.Public.Data.Entities;
using Xyzies.TWC.Public.Data.Repositories.Interfaces;

namespace Xyzies.TWC.Public.Api.Managers
{
    /// <inheritdoc />
    public class BranchManager : IBranchManager
    {

        private readonly ILogger<BranchManager> _logger = null;
        private readonly IBranchRepository _branchRepository = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="branchRepository"></param>
        public BranchManager(ILogger<BranchManager> logger, IBranchRepository branchRepository)
        {
            _logger = logger;
            _branchRepository = branchRepository;

        }
        /// <summary>
        /// Getting all branches or first 15 by defolt
        /// </summary>
        public async Task<PagingResult<BranchModel>> GetBranches(BranchFilter filter, Sortable sortable, Paginable paginable)
        {
            IQueryable<Branch> queryable = await _branchRepository.GetAsync();

            if (!string.IsNullOrEmpty(filter.State))
            {
                queryable = queryable.Where(x => x.State.ToLower().Equals(filter.State.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.City))
            {
                queryable = queryable.Where(x => x.City.ToLower().Contains(filter.City.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                queryable = queryable.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.BranchName))
            {
                queryable = queryable.Where(x => x.BranchName.ToLower().Contains(filter.BranchName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter.BranchId))
            {
                queryable = queryable.Where(x => x.Id.Equals(filter.BranchId));
            }

            // Calculate total count
            var queryableCount = queryable;
            int totalCount = queryableCount.Count();

            // Sorting
            if (sortable.SortBy.ToLower() == "createddate" )
            {
                if (sortable.SortOrder.ToLower().Equals("desc"))
                {
                    queryable = queryable.OrderByDescending(x => x.CreatedDate);
                }
                else queryable = queryable.OrderBy(x => x.CreatedDate);
            }

            if (sortable.SortBy.ToLower() == "status")
            {
                if (sortable.SortOrder.Equals("desc"))
                {
                    queryable = queryable.OrderBy(x => x.Status);
                }
                else queryable = queryable.OrderBy(x => x.CreatedDate);
            }
            if (sortable.SortBy.ToLower() == "branchname")
            {
                if (sortable.SortOrder.Equals("desc"))
                {
                    queryable = queryable.OrderBy(x => x.BranchName);
                }
                else queryable = queryable.OrderBy(x => x.CreatedDate);
            }

            // Pagination
            queryable = queryable.Skip(paginable.Skip.Value).Take(paginable.Take.Value);

            return new PagingResult<BranchModel>
            {
                Total = totalCount,
                ItemsPerPage = paginable.Take.Value,
                Data = queryable.ToList().Adapt<List<BranchModel>>()
            };
        }
    }
}
