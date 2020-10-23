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
        private readonly IRouteService _Routeservice;

        public RouteController(IRouteService Routeservice)
        {
            _Routeservice = Routeservice;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetRoute()
        {
            IEnumerable<Route> RouteList = _Routeservice.GetRoute();

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
            Route route = _Routeservice.GetRoute(id);
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
                Route route = new Route();
                route.Distance = dto.Distance;
                route.MapId = dto.MapId;
                route.FirstCityId = dto.FirstCityId;
                route.SecondCityId = dto.SecondCityId;
                _Routeservice.CreateRoute(route);
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
                Route route = _Routeservice.GetRoute(id);
                route.Distance = dto.Distance;
                route.MapId = dto.MapId;
                route.FirstCityId = dto.FirstCityId;
                route.SecondCityId = dto.SecondCityId;
                _Routeservice.UpdateRoute(route);
                return route;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
