using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Linq;
using API.Models;

namespace API.Controllers
{
    [Route("/{controller}/")]
    [ApiController]
    public class MapController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult<API.Models.Map>> MapCreateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpPut]
        public async Task<ActionResult<API.Models.Map>> MapUpdateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<API.Models.Map>> MapGetResponse(Guid Id)
        {
            return new API.Models.Map(Id,"test", new Guid());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> MapDeleteResponse(Guid id)
        {
            return Ok();
        }
    }
}
