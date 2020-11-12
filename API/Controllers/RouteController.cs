using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services.Interfaces;
using Service.DTO;

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
            var RouteList = _routeservice.GetRoutes();

            if (RouteList.Count() == 0)
            {
                return NotFound();
            }
            return Ok(RouteList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<RouteGetDTO> GetRoute(Guid id)
        {
            var route = _routeservice.GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            return route;
        }

        [HttpPost]
        public ActionResult<RouteGetDTO> CreateRoute([FromBody] RouteCreateDTO dto)
        {
            try
            {
                return _routeservice.CreateRoute(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<RouteCreateDTO> UpdateRoute(Guid id, [FromBody] RouteCreateDTO dto)
        {
            try
            {
                return _routeservice.UpdateRoute(id, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}