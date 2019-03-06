using System.Threading.Tasks;
using IdentityServiceClient.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace IdentityServiceClient.Middlewares
{
    public class HttpContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IIdentityManager _manager;

        public HttpContextMiddleware(RequestDelegate next, IIdentityManager manager)
        {
            _manager = manager;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _manager.Context = context;
            await _next(context);
        }
    }
    public static class HttpContextMiddlewareExtension
    {
        public static IApplicationBuilder UseClientMiddleware(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<HttpContextMiddleware>();
        }
    }
}
