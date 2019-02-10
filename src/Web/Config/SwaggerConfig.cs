using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IO;
using System.Linq;

namespace Web.Config
{
    public static class SwaggerConfig
    {
        public static bool IsEnabled(IConfiguration configuration) => !configuration.GetValue<bool>("Swagger:Disable");

        public static IServiceCollection AddSwagger(this IServiceCollection services, IHostingEnvironment hosting, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                        "v1.0",
                        new Info
                        {
                            Version = "1.0",
                            Description = $"You are viewing version '1.0' of API. All API versions: ['1.0']."
                        });

                options.DescribeAllEnumsAsStrings();

                // documentation from XML comments
                var xmlFile = Directory
                    .GetFiles(hosting.ContentRootPath, "Web.xml", SearchOption.AllDirectories)
                    .FirstOrDefault();
                if (xmlFile != null)
                    options.IncludeXmlComments(xmlFile);
            });
            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            if (!IsEnabled(configuration)) return app;

            return app
                .UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/schema.json")
                .UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint($"/swagger/v1.0/schema.json", $"API version 1.0");
            });
        }
    }
}
