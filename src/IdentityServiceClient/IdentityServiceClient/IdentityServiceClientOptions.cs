using Microsoft.AspNetCore.Http;

namespace IdentityServiceClient
{
    public class IdentityServiceClientOptions
    {
        public string ServiceUrl { get; set; }
        public HttpContext Context { get; set; }
    }
}
