using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Web.Config;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseApplicationInsights()
                .ConfigureAppConfiguration(SettingsConfig.AddProviders)
                .ConfigureLogging(LoggingConfig.AddLogging)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
