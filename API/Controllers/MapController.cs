using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _Mapservice;
        private readonly IImageService _Imageservice;

        public MapController(IMapService Mapservice, IImageService Imageservice)
        {
            _Mapservice = Mapservice;
            _Imageservice = Imageservice;
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Map>> GetMap(Guid id)
        {
            try
            {
                Map map = _Mapservice.GetMap(id);
                if (map == null)
                {
                    return NotFound();
                }
                   
                return map;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("getall")]
        public IEnumerable<Map> GetMap() => _Mapservice.GetMap();

        [HttpPost]
        public async Task<ActionResult<Map>> CreateMap()
        {
            try
            {
                string Name = Request.Form["Name"];
                string ImageId = Request.Form["ImageId"];
                Map map = new Map();

                if (!(Name is null))
                {
                    map.Name = Name;
                    if (!(ImageId is null))
                    {
                        MapImage img = _Imageservice.GetImage(Guid.Parse(ImageId));
                        map.Image = img;
                    }

                    _Mapservice.CreateMap(map);
                }
                else
                {
                    return BadRequest();
                }
                return map;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Map>> UpdateMap(Guid Id)
        {
            try
            {
                string Name = Request.Form["Name"];
                string ImageId = Request.Form["ImageId"];
                Map map = _Mapservice.GetMap(Id);
                if (map == null)
                {
                    return NotFound();
                }

                if (!(Name is null))
                {
                    map.Name = Name;
                    map.Id = Id;
                    if (!(ImageId is null))
                    {
                        MapImage img = _Imageservice.GetImage(Guid.Parse(ImageId));
                        map.Image = img;
                    }

                    map = _Mapservice.UpdateMap(map);
                }
                else
                {
                    return BadRequest();
                }

                return map;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Map>> DeleteMap(Guid Id)
        {
            try
            {
                _Mapservice.DeleteMap(Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }


}
