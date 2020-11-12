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
            Log.Information("Index page");
            return "API Running...";
            //Todo: maybe add AspNetCore.RouteAnalyzer
        }

    }
}
