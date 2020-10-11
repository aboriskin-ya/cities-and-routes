using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        public string ApiIsRuning()
        {
            return "API Running...";
            //Todo: maybe add AspNetCore.RouteAnalyzer
        }

    }
}
