using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.PathResolver;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/pathresolver")]
    [ApiController]
    public class PathResolverController : ControllerBase
    {
        private readonly IAlgorithmService _algorithmService;
        private readonly ITimeCounterService _timeCounterService;
        public PathResolverController(IAlgorithmService AlgorithmService, ITimeCounterService timeCounterService)
        {
            _algorithmService = AlgorithmService;
            _timeCounterService = timeCounterService;
        }

        [ProducesResponseType(typeof(ShortestPathResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("FindShortestPath")]
        public IActionResult FindShortestPath([FromBody] PathResolverDTO Dto)
        {
            var result = _algorithmService.FindShortestPath(Dto.MapId, Dto.CityFromId, Dto.CityToId);
            _timeCounterService.Stop();
            if (result == null)
                result = new ShortestPathResponseDTO() { IsPathFound = false };
            else
                result.IsPathFound = true;
            result.ProcessDuration = _timeCounterService.GetTime();
            return Ok(result);
        }

        [ProducesResponseType(typeof(TravelSalesmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("solve-travel-salesman-annealing")]
        public async Task<IActionResult> SolveTravelSalesmanAnnealing([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = await _algorithmService.SolveAnnealingTravelSalesman(BodyRequest);
            _timeCounterService.Stop();
            if (response == default) return BadRequest();
            response.ProcessDuration = _timeCounterService.GetTime();
            return Ok(response);
        }

        [ProducesResponseType(typeof(TravelSalesmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("solve-travel-salesman-nearest")]
        public async Task<IActionResult> SolveTravelSalesmanNearest([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var response = await _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest);
            _timeCounterService.Stop();
            if (response == default) return BadRequest();
            response.ProcessDuration = _timeCounterService.GetTime();
            return Ok(response);
        }

        [ProducesResponseType(typeof(TravelSalesmanResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("solve-travel-salesman-quickest")]
        public IActionResult Experiment([FromBody] TravelSalesmanRequest BodyRequest)
        {
            var taskArr = new Task<TravelSalesmanResponse>[]
            {
                _algorithmService.SolveNearestNeghborTravelSalesman(BodyRequest),
                _algorithmService.SolveAnnealingTravelSalesman(BodyRequest)
            };

            var task = Task.WhenAny(taskArr).Result;
            var response = task.Result;
            _timeCounterService.Stop();
            response.ProcessDuration = _timeCounterService.GetTime();
            return Ok(response);
        }


    }
}
