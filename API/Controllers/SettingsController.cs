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
    [Route("api/settings")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;
        public SettingsController(ISettingsService service)
        {
            _service = service;
        }

        [ProducesResponseType(typeof(IEnumerable<SettingsGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("getall")]
        public IActionResult Get()
        {
            var settings = _service.GetSettings();

            if (settings.Count() == 0)
            {
                return NotFound(null);
            }

            return Ok(settings);
        }

        [ProducesResponseType(typeof(SettingsGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var settings = _service.GetSettings(id);

            if (settings == null)
            {
                return NotFound(null);
            }

            return Ok(settings);
        }

        [ProducesResponseType(typeof(SettingsGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("~/api/map/{id:guid}/settings")]
        public IActionResult GetMap(Guid id)
        {
            var settings = _service.GetSettingsOfMap(id);

            if (settings == null)
            {
                return NotFound(null);
            }

            return Ok(settings);
        }

        [ProducesResponseType(typeof(SettingsGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public IActionResult Post([FromBody] SettingsCreateDTO settingsDTO)
        {
            var dto = _service.CreateSettings(settingsDTO);
            return Ok(dto);
        }

        [ProducesResponseType(typeof(SettingsGetDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody] SettingsUpdateDTO settingsDTO)
        {
            var dto = _service.UpdateSettings(id, settingsDTO);
            return Ok(dto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (_service.DeleteSettings(id))
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