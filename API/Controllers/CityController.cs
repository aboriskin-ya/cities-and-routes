using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.Interfaces;

namespace API.Controllers
{
    [Route("city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _Cityservice;

        public CityController(ICityService CityService)
        {
            _Cityservice = CityService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CityGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:Guid}")]
        public IActionResult GetCity(Guid id)
        {
            var city = _Cityservice.GetCity(id);
            if (city == null)
            {
                return NotFound();
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
            var CityList = _Cityservice.GetCities();

            if (CityList.Count() == 0)
            {
                return NotFound();
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
            return Ok(_Cityservice.CreateCity(dto));
        }

        [ProducesResponseType(typeof(CityGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateCity(Guid id, [FromBody] CityCreateDTO city)
        {
            return Ok(_Cityservice.UpdateCity(id, city));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteCity(Guid id)
        {
            if (_Cityservice.DeleteCity(id))
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