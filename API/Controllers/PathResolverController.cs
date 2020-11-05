using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service;
using DataAccess.DTO;
using System;

namespace API.Controllers
{
    [Route("{controller}")]
    [ApiController]
    public class PathResolverController : ControllerBase
    {
        private readonly IPathResolverService _pathResolverservice;

        public PathResolverController(IPathResolverService PathResolverService)
        {
            _pathResolverservice = PathResolverService;
        }

        [HttpPost]
        [Route("FindPath")]
        public IActionResult FindPath([FromBody] PathResolverDTO dto)
        {
            return Ok(_pathResolverservice.FindPath(dto.mapId, dto.cityFromId, dto.cityToId));
        }

    }
}
