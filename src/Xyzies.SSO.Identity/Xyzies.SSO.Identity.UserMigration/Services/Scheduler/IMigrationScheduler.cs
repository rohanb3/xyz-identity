using Microsoft.Extensions.Hosting;
using System;

namespace Xyzies.SSO.Identity.CPUserMigration.Services.Scheduler
{
    public interface IMigrationScheduler : IHostedService, IDisposable
    {
    }
}
