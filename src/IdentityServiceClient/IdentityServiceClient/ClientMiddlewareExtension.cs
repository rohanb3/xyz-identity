using System;
using IdentityServiceClient.Service;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServiceClient
{
    public static class ClientMiddlewareExtension
    {
        /// <summary>
        /// Needs to setup ServiceApiUri, Add Middleware HttpContextMiddlewareExtension.UseHttpContextManager()
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityClient(this IServiceCollection services, Action<IdentityServiceClientOptions> setupAction)
        {
            //Let the caller configure us.
            var options = new IdentityServiceClientOptions();
            setupAction(options);

            services.AddMemoryCache();
            services.AddSingleton<IdentityServiceClientOptions, IdentityServiceClientOptions>();
            return services.AddSingleton<IIdentityManager>(im =>
            {
                var memoryCacheService = im.GetService<IMemoryCache>();
                return new IdentityManager(options);
            });
        }
    }
}
