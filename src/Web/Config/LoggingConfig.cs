using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace Web.Config
{
    internal class LoggingConfig
    {
        public static void AddLogging(WebHostBuilderContext context, ILoggingBuilder builder)
        {
            builder.AddSerilog(new LoggerConfiguration()
                .ReadFrom
                .Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("app", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("machine", Environment.MachineName)
                .CreateLogger());
        }
    }
}
