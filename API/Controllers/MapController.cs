﻿using AutoMapper;
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
        private readonly IMapService _Mapservice;
        private readonly IMapper _mapper;

        public MapController(IMapService MapService, IMapper Mapper)
        {
            _Mapservice = MapService;
            _mapper = Mapper;
        }

        [ProducesResponseType(typeof(MapGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(Map), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<Map> CreateMap([FromBody] MapCreateDTO dto)
        {
            return _Mapservice.CreateMap(dto);
        }

        [ProducesResponseType(typeof(Map), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:Guid}")]
        public ActionResult<Map> UpdateMap(Guid id, [FromBody] MapCreateDTO dto)
        {
            return _Mapservice.UpdateMap(dto, id);
        }

        [ProducesResponseType(typeof(Map), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
