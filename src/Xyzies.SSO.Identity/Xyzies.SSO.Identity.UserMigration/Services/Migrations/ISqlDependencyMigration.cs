using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xyzies.SSO.Identity.CPUserMigration.Services.Migrations
{
    public interface ISqlDependencyMigration
    {
        Task Initialize();
    }
}
