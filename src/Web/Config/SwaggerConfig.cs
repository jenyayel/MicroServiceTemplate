using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Linq;

namespace Web.Config
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IHostingEnvironment hosting)
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

        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
        {
            return app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.DocExpansion("none");
                c.SwaggerEndpoint($"/swagger/v1.0/schema.json", $"API version 1.0");
            });
        }
    }
}
