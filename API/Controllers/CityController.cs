using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using AutoMapper;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _Cityservice;
        private readonly IMapper _mapper;

        public CityController(ICityService CityService, IMapper Cityper)
        {
            _Cityservice = CityService;
            _mapper = Cityper;
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
                 City city = _mapper.Map<City>(dto);
                _Cityservice.CreateCity(city);
                return city;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<City> UpdateCity(Guid id, [FromBody] CityDTO dto)
        {
            try
            {
                City city = _Cityservice.GetCity(id);
                _mapper.Map<CityDTO, City>(dto, city);
                _Cityservice.UpdateCity(city);
                return city;
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
