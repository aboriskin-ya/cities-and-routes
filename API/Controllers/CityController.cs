using System;
using System.Linq;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.Interfaces;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService CityService)
        {
            _service = CityService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<CityGetDTO> GetCity(Guid id)
        {
            var city = _service.GetCity(id);
            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetCity()
        {
            var CityList = _service.GetCities();

            if (CityList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(CityList);
        }

        [HttpPost]
        public ActionResult<CityGetDTO> CreateCity([FromBody] CityCreateDTO dto)
        {
            try
            {
                return _service.CreateCity(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<CityCreateDTO> UpdateCity(Guid id, [FromBody] CityCreateDTO city)
        {
            try
            {
                return _service.UpdateCity(id, city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult DeleteCity(Guid id)
        {
            if (_service.DeleteCity(id))
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