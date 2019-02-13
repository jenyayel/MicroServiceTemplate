using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using Web;

namespace Integration.Utils
{
    public class TestsContext : WebApplicationFactory<Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return base.CreateWebHostBuilder()
                .UseEnvironment("Test")
                .ConfigureAppConfiguration((c, b) =>
                {
                    b
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false);
                });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // set fake service and DB here
            });
        }
    }
}