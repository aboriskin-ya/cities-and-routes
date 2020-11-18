using AutoMapper;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services.Interfaces;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _service;
        private readonly IMapper _mapper;

        public MapController(IMapService MapService, IMapper Mapper)
        {
            _service = MapService;
            _mapper = Mapper;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public ActionResult<MapGetDTO> GetMap(Guid id)
        {
            MapGetDTO map = _service.GetMap(id);
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
            var MapList = _service.GetMaps();
            if (MapList.Count() == 0)
            {
                return NotFound();
            }
            return Ok(MapList);
        }

        [HttpGet]
        [Route("getallnames")]
        public IActionResult GetMapName()
        {
            var MapList = _service.GetMapsNames();
            if (MapList.Count() == 0)
            {
                return NotFound();
            }
            return Ok(MapList);
        }

        [HttpPost]
        public ActionResult<MapGetDTO> CreateMap([FromBody] MapCreateDTO dto)
        {
            try
            {
                return _service.CreateMap(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<MapCreateDTO> UpdateMap(Guid id, [FromBody] MapCreateDTO dto)
        {
            try
            {
                return _service.UpdateMap(id, dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public ActionResult DeleteMap(Guid id)
        {
            if (_service.DeleteMap(id))
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