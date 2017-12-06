using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class StatusController : Controller
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogDebug("Status called {@caller}", new
            {
                Ip = this.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                Headers = this.Request.Headers
            });

            return "OK";
        }
    }
}
