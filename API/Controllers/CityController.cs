using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _Cityservice;

        public CityController(ICityService CityService)
        {
            _Cityservice = CityService;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<City> GetCity(Guid id)
        {
            City city = _Cityservice.GetCity(id);
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
            IEnumerable<City> CityList = _Cityservice.GetCity();

            if (CityList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(CityList);
        }

        [HttpPost]
        public ActionResult<City> CreateCity([FromBody] CityDTO dto)
        {
            try
            {
                return _Cityservice.CreateCity(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<City> UpdateCity(Guid id, [FromBody] CityDTO city)
        {
            try
            {
                return _Cityservice.UpdateCity(id, city);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult<City> DeleteCity(Guid id)
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
