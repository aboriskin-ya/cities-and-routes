using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/")]
    [ApiController]
    public class MapController : ControllerBase
    {

        [HttpPost("MapCreateRequest")]
        public async Task<ActionResult<API.Models.Map>> MapCreateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpPost("MapUpdateRequest")]
        public async Task<ActionResult<API.Models.Map>> MapUpdateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpPost("MapGetResponse")]
        public async Task<ActionResult<API.Models.Map>> MapGetResponse([FromBody] Guid id)
        {
            return new API.Models.Map(id);
        }

        [HttpPost("MapImageUploadRequest")]
        public async Task<ActionResult<API.Models.Image>> MapImageUploadRequest([FromBody] Guid id)
        {
            return new API.Models.Image(id);
        }

        [HttpPost("MapImageIdGetResponse")]
        public async Task<ActionResult<API.Models.Image>> MapImageIdGetResponse([FromBody] Guid id)
        {
            return new API.Models.Image(id);
        }

    }
}
