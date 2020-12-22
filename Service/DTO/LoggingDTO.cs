using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTO
{
    public class LoggingDTO
    {
        public string ExceptionType { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
