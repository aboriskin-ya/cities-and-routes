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
    [Route("settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;
        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(SettingsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(SettingsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

        [ProducesResponseType(typeof(SettingsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<SettingsDTO> Post([FromBody] SettingsDTO settingsDTO)
        {
            _service.CreateSettings(settingsDTO);
            return settingsDTO;
        }

        [ProducesResponseType(typeof(SettingsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:guid}")]
        public ActionResult<SettingsDTO> Put(Guid id, [FromBody] SettingsUpdateDTO settingsDTO)
        {
            var dto = _service.UpdateSettings(id, settingsDTO);
            return dto;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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