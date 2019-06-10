using System;
using System.Collections.Generic;
using System.Text;
using Xyzies.SSO.Identity.Data.Entity;

namespace Xyzies.SSO.Identity.Data.Repository
{
   public interface ICpRoleRepository : IRepository<Guid, CPRole>, IDisposable
    {
    }
}
