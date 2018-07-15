using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Web.Config;
using Web.Middleware;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnv;
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            _configuration = configuration;
            _hostingEnv = env;
            _loggerFactory = loggerFactory;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAndConfigureMvc()
                .AddSwagger(_hostingEnv, _configuration);
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory)
        {
            if (!_hostingEnv.IsProduction())
                app.UseDeveloperExceptionPage();

            if (SwaggerConfig.IsEnabled(_configuration))
            {
                // static files only required for Swagger
                app
                    .UseDefaultFiles()
                    .UseStaticFiles();
            }

            app
                .UseMiddleware<LogRequestIdMiddleware>()
                .UseMvc()
                .UseSwagger(_configuration);
        }
    }
}
