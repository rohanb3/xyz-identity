using Microsoft.Extensions.Configuration;
using Xyzies.SSO.Identity.API;

namespace Xyzies.SSO.Identity.Tests.Infrostructure
{
    public class TestStartUp : Startup
    {
        public TestStartUp(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
