﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core;
using Service.DTO;

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
        [HttpPost]
        public IActionResult LoggingWPF([FromBody] LoggingDTO loggingDTO)
        {
            Guid guidError = Guid.NewGuid();
            using (LogContext.PushProperty("ExceptionStackTrace", loggingDTO.ExceptionStackTrace))
            {
                _logger.LogError("Wpf exception message: {@guidError}, {@ExceptionMessage}, {@ExceptionType}",
                guidError.ToString(), loggingDTO.ExceptionMessage, loggingDTO.ExceptionType);
            }
            return Ok(guidError);
        }
    }
}
