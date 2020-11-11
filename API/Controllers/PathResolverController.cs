using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.PathResolver;
using Service.Services.Interfaces;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class PathResolverController : ControllerBase
    {
        private readonly IAlgorithmService _algorithmService;

        public PathResolverController(IAlgorithmService AlgorithmService)
        {
            _algorithmService = AlgorithmService;
        }

        [HttpPost]
        [Route("FindShortestPath")]
        public IActionResult FindShortestPath([FromBody] PathResolverDTO Dto)
        {
            return Ok(_algorithmService.FindShortestPath(Dto.MapId, Dto.CityFromId, Dto.CityToId));
        }

        [HttpPost]
        [Route("solve-travel-salesman-annealing")]
        public IActionResult SolveTravelSalesmanAnnealing([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = _algorithmService.SolveAnnealingTravelSalesman(BodyRequest);
            if (response == default) return BadRequest();
            return Ok(response);

        }
        [HttpPost]
        [Route("solve-travel-salesman-nearest")]
        public IActionResult SolveTravelSalesmanNearest([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest);
            if (response == default) return BadRequest();
            return Ok(response);
        }
        [HttpPost]
        [Route("experiment")]
        public IActionResult Experiment([FromBody] TravelSalesmanRequest BodyRequest)
        {
            TravelSalesmanResponse response = new TravelSalesmanResponse(); ;
            var taskArr = new Task[] {
                new Task(() => response= _algorithmService.SolveAnnealingTravelSalesman(BodyRequest)),
                new Task(() => response= _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest))
            };
            foreach (var task in taskArr)
            {
                task.Start();
                Task.WaitAny(taskArr);
            }
            return Ok(response);
        }


    }
}
