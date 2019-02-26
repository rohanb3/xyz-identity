﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xyzies.TWC.Public.Data.Entities;
using Xyzies.TWC.Public.Data.Repositories.Interfaces;

namespace Xyzies.TWC.Public.Data.Repositories
{
    public class BranchRepository : EfCoreBaseRepository<int, Branch>, IBranchRepository
    {
        public BranchRepository(AppDataContext dbContext)
            : base(dbContext)
        {

        }

        public override async Task<Branch> GetAsync(int id)
        {
            var branches = await Data
                .Include(x => x.BranchContacts)
                .FirstOrDefaultAsync(entity => entity.Id.Equals(id));

            return branches;
        }

        /// <inheritdoc />
        //public override int Add(Branch entity)
        //{
        //   var id = Data.Add(entity).Entity.Id;
        //   return id;
        //}
    }
}
