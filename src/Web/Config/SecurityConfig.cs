using Microsoft.Extensions.DependencyInjection;

namespace Web.Config
{
    public static class SecurityConfig
    {
        public const string POLCY_NAME = "default";

        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            return services
               .AddCors(c =>
                   c.AddPolicy(
                       POLCY_NAME,
                       p => p.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials())
               );
        }
    }
}
