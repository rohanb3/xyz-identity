using Ardas.AspNetCore.Logging.Formatters;
using Ardas.AspNetCore.Logging.Models;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Network;
using System;

namespace Ardas.AspNetCore.Logging
{
    public static class LoggerExtensions
    {
        public static IServiceCollection AddTcpStreamLogging(this IServiceCollection services, Action<TcpLogOptions> setupOptions, LogEventLevel logLevel = LogEventLevel.Information, ILogEventEnricher[] enriches = null)
        {
            TcpLogOptions options = new TcpLogOptions();
            setupOptions(options);

            var protocol = options.SecureConnection ? "tls" : "tcp";
            var uri = $"{protocol}://{options.Ip}:{options.Port}";

            var kibanaLogsFormatter = new KibanaLogsFormatter();
            var loggerConfig = new LoggerConfiguration()
                                .MinimumLevel.Is(logLevel)
                                .MinimumLevel.Override("System", logLevel)
                                .MinimumLevel.Override("Microsoft", logLevel)
                                .WriteTo.Console(kibanaLogsFormatter)
                                .WriteTo.TCPSink(uri, kibanaLogsFormatter);

            if ((enriches?.Length ?? 0) > 0)
            {
                loggerConfig.Enrich.With(enriches);
            }

            Log.Logger = loggerConfig.CreateLogger();

            services.AddLogging(loggingBuilder => loggingBuilder
                                        .AddSerilog(dispose: options.Dispose));

            return services;
        }
    }
}
