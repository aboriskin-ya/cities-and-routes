using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.PathResolver;
using Service.Services.Interfaces;

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
        [Route("solve-travel-salesman")]
        public IActionResult SolveTravelSalesman([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var guidCollection = _algorithmService.SolveTravelSalesman(BodyRequest);
            if (guidCollection == default) return BadRequest();
            return Ok(guidCollection);
        }


    }
}
