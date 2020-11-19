using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services.Interfaces;
using Service.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("route")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _service;

        public RouteController(IRouteService Routeservice)
        {
            _service = Routeservice;
        }

        [ProducesResponseType(typeof(IEnumerable<RouteGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(RouteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetRoute(Guid id)
        {
            var route = _service.GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            return Ok(route);
        }

        [ProducesResponseType(typeof(RouteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult CreateRoute([FromBody] RouteCreateDTO dto)
        {
            return Ok(_routeservice.CreateRoute(dto));
        }

        [ProducesResponseType(typeof(RouteGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateRoute(Guid id, [FromBody] RouteCreateDTO dto)
        {
            return Ok(_routeservice.UpdateRoute(id, dto));
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