using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Web.Middleware
{
    /// <summary>
    /// Enriches logger with request identifier, so every log message written
    /// within same HTTP request will contain this identifier
    /// </summary>
    public class LogRequestIdMiddleware
    {
        private readonly RequestDelegate _next;

        public LogRequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("requestId", context.Request.HttpContext.TraceIdentifier))
            {
                await _next.Invoke(context);
            }
        }
    }
}
