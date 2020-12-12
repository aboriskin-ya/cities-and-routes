using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/log")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Route("{type:string}/{mess:string}/{stacktrace:string}")]
        public IActionResult LoggingWPF(string type, string mess, string stacktrace)
        {
            Guid guidError = new Guid();
            _logger.LogInformation($"Wpf exception message: type {type}, message {mess}, stacktrace {stacktrace}, Guid {guidError}");
            return Ok(guidError);
        }
    }
}
