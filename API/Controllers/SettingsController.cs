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
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;
        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getall")]
        public ActionResult Get()
        {
            IEnumerable<SettingsDTO> settings = _service.GetSettings();

            if (settings.Count() == 0)
            {
                return NotFound();
            }

            return Ok(settings);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public ActionResult<SettingsDTO> Get(Guid id)
        {
            SettingsDTO settings = _service.GetSettings(id);

            if (settings == null)
            {
                return NotFound();
            }

            return settings;
        }

        [HttpGet]
        [Route("~/map/{id:guid}/settings")]
        public ActionResult<SettingsDTO> GetMap(Guid id)
        {
            SettingsDTO settings = _service.GetSettingsOfMap(id);

            if (settings == null)
            {
                return NotFound();
            }

            return settings;
        }

        [HttpPost]
        public ActionResult<SettingsDTO> Post([FromBody] SettingsDTO settingsDTO)
        {
            try
            {
                _service.CreateSettings(settingsDTO);
                return settingsDTO;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult<SettingsDTO> Put(Guid id, [FromBody] SettingsDTO settingsDTO)
        {
            try
            {
                SettingsDTO settings = _service.GetSettings(id);

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
