using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Controllers
{
    /// <summary>
    /// Exposes stats of API
    /// </summary>
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Basic health of the application
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        public string Get()
        {
            _logger.LogDebug("Status called {@caller}", new
            {
                Ip = this.Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                this.Request.Headers
            });

            return "OK";
        }
    }
}
