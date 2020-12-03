using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService CityService)
        {
            _service = CityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CityGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:Guid}")]
        public IActionResult GetCity(Guid id)
        {
            var city = _service.GetCity(id);
            if (city == null)
            {
                return NotFound(null);
            }

            return Ok(city);
        }

        [ProducesResponseType(typeof(IEnumerable<CityGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("getall")]
        public IActionResult GetCity()
        {
            var CityList = _service.GetCities();

            if (CityList.Count() == 0)
            {
                return NotFound(null);
            }

            return Ok(CityList);
        }

        [ProducesResponseType(typeof(CityGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult CreateCity([FromBody] CityCreateDTO dto)
        {
            return Ok(_service.CreateCity(dto));
        }

        [ProducesResponseType(typeof(CityGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateCity(Guid id, [FromBody] CityCreateDTO city)
        {
            return Ok(_service.UpdateCity(id, city));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteCity(Guid id)
        {
            if (_service.DeleteCity(id))
            {
                return Ok();
            }
            else
            {
                return NotFound(null);
            }
        }
    }
}