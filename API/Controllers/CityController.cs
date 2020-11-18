using System;
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
        public ActionResult<CityGetDTO> GetCity(Guid id)
        {
            var city = _Cityservice.GetCity(id);
            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
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

        [ProducesResponseType(typeof(CityCreateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<CityCreateDTO> CreateCity([FromBody] CityCreateDTO dto)
        {
            return _Cityservice.CreateCity(dto);
        }

        [ProducesResponseType(typeof(CityCreateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<CityCreateDTO> UpdateCity(Guid id, [FromBody] CityCreateDTO city)
        {
            return _Cityservice.UpdateCity(id, city);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult DeleteCity(Guid id)
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