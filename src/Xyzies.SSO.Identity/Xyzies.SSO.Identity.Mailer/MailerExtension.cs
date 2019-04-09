using Microsoft.Extensions.DependencyInjection;
using System;
using Xyzies.SSO.Identity.Mailer.Helpers;
using Xyzies.SSO.Identity.Mailer.Services;

namespace Xyzies.SSO.Identity.Mailer
{
    public static class MailerExtension
    {
        public static IServiceCollection AddMailer(this IServiceCollection services, Action<MailerOptions> setupOptions)
        {
            services.Configure(setupOptions);

            services.AddScoped<IMailerService, MailerService>();

            return services;
        }
    }
}
