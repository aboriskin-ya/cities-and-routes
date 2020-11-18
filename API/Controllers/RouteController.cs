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
        private readonly IRouteService _service;

        public RouteController(IRouteService Routeservice)
        {
            _service = Routeservice;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetRoute()
        {
            var RouteList = _service.GetRoutes();

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
            var route = _service.GetRoute(id);
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
                return _service.CreateRoute(dto);
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
                return _service.UpdateRoute(id, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult DeleteRoute(Guid id)
        {
            if (_service.DeleteRoute(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}