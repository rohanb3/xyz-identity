﻿using System;
using Xyzies.TWC.Public.Data.Core;
using Xyzies.TWC.Public.Data.Entities;

namespace Xyzies.TWC.Public.Data.Repositories.Interfaces
{
    public interface ICompanyRepository : IRepository<Guid, Company>, IDisposable
    {
    }
}
