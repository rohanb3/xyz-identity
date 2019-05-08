using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Network;
using System;
using Xyzies.Logger.Models;

namespace Xyzies.Logger
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddTcpStreamLogging(this IServiceCollection services, Action<TcpLogOptions> setupOptions, LogEventLevel logLevel = LogEventLevel.Information, ILogEventEnricher[] enriches = null)
        {
            TcpLogOptions options = new TcpLogOptions();
            setupOptions(options);

            var protocol = options.SecureConnection ? "tls" : "tcp";
            var uri = $"{protocol}://{options.Ip}:{options.Port}";

            var loggerConfig = new LoggerConfiguration()
                                .MinimumLevel.Is(logLevel)
                                .WriteTo.TCPSink(uri);

            if ((enriches?.Length ?? 0) > 0)
            {
                loggerConfig.Enrich.With(enriches);
            }

            Log.Logger = loggerConfig.CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: options.Dispose));

            return services;
        }
    }
}
