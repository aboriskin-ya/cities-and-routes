using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _Mapservice;
        private readonly IMapper _mapper;

        public MapController(IMapService MapService, IMapper Mapper)
        {
            _Mapservice = MapService;
            _mapper = Mapper;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<MapGetDTO> GetMap(Guid id)
        {
            MapGetDTO map = _Mapservice.GetMap(id);
            if (map == null)
            {
                return NotFound();
            }
            return map;
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetMap()
        {
            IEnumerable<MapGetDTO> MapList = _Mapservice.GetMaps();
            if (MapList.Count() == 0)
            {
                return NotFound();
            }
            return Ok(MapList);
        }

        [HttpPost]
        public ActionResult<Map> CreateMap([FromBody] MapCreateDTO dto)
        {
            try
            {
                return _Mapservice.CreateMap(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<Map> UpdateMap(Guid id, [FromBody] MapCreateDTO dto)
        {
            try
            {
                return _Mapservice.UpdateMap(dto, id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult<Map> DeleteMap(Guid id)
        {
            if (_Mapservice.DeleteMap(id))
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
