using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DataAccess.DTO;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getall")]
        public ActionResult Get()
        {
            IEnumerable<Settings> settings = _service.GetSettings();

            if (settings.Count() == 0)
            {
                return NotFound();
            }

            return Ok(settings);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<Settings> Get(Guid id)
        {
            Settings settings = _service.GetSettings(id);

            if (settings == null)
            {
                return NotFound();
            }

            return settings;
        }

        [HttpGet]
        [Route("~/map/{id:guid}/settings")]
        public ActionResult<Map> GetMap(Guid id)
        {
            Map map = _service.GetMap(id);

            if (map == null)
            {
                return NotFound();
            }

            return map;
        }

        [HttpPost]
        public ActionResult<Settings> Post([FromBody] SettingsDTO settingsDTO)
        {
            try
            {
                Settings settings = _mapper.Map<Settings>(settingsDTO);
                _service.CreateSettings(settings);
                return settings;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult<Settings> Put(Guid id, [FromBody] SettingsDTO settingsDTO)
        {
            try
            {
                Settings settings = _service.GetSettings(id);

                _mapper.Map(settingsDTO, settings);

                _service.UpdateSettings(settings);
                return settings;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if (_service.DeleteSettings(id))
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
