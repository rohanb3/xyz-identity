using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
using System.Net.Http;
using Xyzies.SSO.Identity.Tests.Infrostructure;

namespace Xyzies.SSO.Identity.Tests
{
    public class TestServerInitializer : IDisposable
    {
        public readonly TestServer Server;
        public readonly HttpClient HttpClient;

        public TestServerInitializer()
        {
            IWebHostBuilder webHostBuild =
                WebHost.CreateDefaultBuilder()
                .UseStartup<TestStartUp>()
                .UseEnvironment("dev")
                .UseWebRoot(Directory.GetCurrentDirectory())
                .UseContentRoot(Directory.GetCurrentDirectory());

            Server = new TestServer(webHostBuild);
            HttpClient = Server.CreateClient();
        }

        public void Dispose()
        {
            Server.Dispose();
            HttpClient.Dispose();
        }
    }
}
