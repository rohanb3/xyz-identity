using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
