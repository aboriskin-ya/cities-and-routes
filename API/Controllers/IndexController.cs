using Microsoft.AspNetCore.Mvc;
using Serilog;

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
