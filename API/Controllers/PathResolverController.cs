using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services.Interfaces;
using DataAccess.DTO;
using System;

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

    }
}
