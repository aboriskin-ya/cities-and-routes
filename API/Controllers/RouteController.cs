using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeservice;

        public RouteController(IRouteService Routeservice)
        {
            _routeservice = Routeservice;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetRoute()
        {
            IEnumerable<Route> RouteList = _routeservice.GetRoute();

            if (RouteList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(RouteList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<Route> GetRoute(Guid id)
        {
            Route route = _routeservice.GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }

            return route;
        }

        [HttpPost]
        public ActionResult<Route> CreateRoute([FromBody] RouteDTO dto)
        {
            try
            {
                Route route = new Route(dto);
                _routeservice.CreateRoute(route);
                return route;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<Route> UpdateRoute(Guid id, [FromBody] RouteDTO dto)
        {
            try
            {
                Route route = _routeservice.GetRoute(id);
                route.Distance = dto.Distance;
                route.MapId = dto.MapId;
                route.FirstCityId = dto.FirstCityId;
                route.SecondCityId = dto.SecondCityId;
                _routeservice.UpdateRoute(route);
                return route;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
