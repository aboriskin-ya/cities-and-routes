using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services.Interfaces;
using DataAccess.DTO;
using System;
using Service.PathResolver;

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
        [Route("appropriateroute")]
        public IActionResult FindShortestPath([FromBody] AppropriateRouteBody BodyRequest)
        {
            var GuidCollection = _algorithmService.SolveTSG(BodyRequest.SelectedCities, BodyRequest.MapId);
            if (GuidCollection == default) return BadRequest();
            return Ok(GuidCollection);
        }
        

    }
}
