using AutoMapper;
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
    [Route("map")]
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

        [ProducesResponseType(typeof(MapGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetMap(Guid id)
        {
            MapGetDTO map = _service.GetMap(id);
            if (map == null)
            {
                return NotFound(null);
            }
            return Ok(map);
        }

        [ProducesResponseType(typeof(IEnumerable<MapGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("getall")]
        public IActionResult GetMap()
        {
            var MapList = _service.GetMaps();
            if (MapList.Count() == 0)
            {
                return NotFound(null);
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
                return NotFound(null);
            }
            return Ok(MapList);
        }

        [ProducesResponseType(typeof(MapGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult CreateMap([FromBody] MapCreateDTO dto)
        {
            return Ok(_service.CreateMap(dto));
        }

        [ProducesResponseType(typeof(MapGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult UpdateMap(Guid id, [FromBody] MapCreateDTO dto)
        {
            return Ok(_service.UpdateMap(dto, id));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteMap(Guid id)
        {
            if (_service.DeleteMap(id))
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