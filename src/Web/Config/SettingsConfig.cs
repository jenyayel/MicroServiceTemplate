using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Web.Config
{
    internal static class SettingsConfig
    {
        public static void AddProviders(WebHostBuilderContext context, IConfigurationBuilder builder)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder
                .SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables("Atrict:");
        }
    }
}
