using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Filters;

namespace Web.Config
{
    public static class MvcConfig
    {
        public static IServiceCollection AddAndConfigureMvc(this IServiceCollection services)
        {
            var builder = services.AddMvcCore(config =>
            {
                config.Filters.Add(typeof(LogExceptionFilter));
            });
            builder.AddAuthorization();
            builder.AddApiExplorer();
            builder.AddDataAnnotations();
            builder.AddJsonFormatters();
            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }
    }
}
