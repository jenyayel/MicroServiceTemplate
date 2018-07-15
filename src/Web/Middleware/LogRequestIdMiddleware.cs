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
        private const string REQUEST_PROPERTY_NAME = "requestId";
        private const string REQUEST_HEADER_NAME = "requestId";
        private readonly RequestDelegate _next;

        public LogRequestIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestId = context.Request.Headers.ContainsKey(REQUEST_HEADER_NAME) ?
                context.Request.Headers[REQUEST_HEADER_NAME].ToString() : context.Request.HttpContext.TraceIdentifier;
            context.Response.Headers.Add(REQUEST_HEADER_NAME, requestId);
            using (LogContext.PushProperty(REQUEST_PROPERTY_NAME, requestId))
            {
                await _next.Invoke(context);
            }
        }
    }
}
