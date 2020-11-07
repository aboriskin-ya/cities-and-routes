using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using Service.Services.Interfaces;
using DataAccess.DTO;

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
        public IActionResult FindPath([FromBody] PathResolverDTO Dto)
        {
            return Ok(_pathResolverservice.FindPath(Dto.MapId, Dto.CityFromId, Dto.CityToId));
        }

    }
}
