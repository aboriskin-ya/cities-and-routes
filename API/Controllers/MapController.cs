using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Linq;

namespace API.Controllers
{
    [Route("/{controller}/")]
    [ApiController]
    public class MapController : ControllerBase
    {

        [HttpPost("MapCreateRequest")]
        public async Task<ActionResult<API.Models.Map>> MapCreateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpPut("MapUpdateRequest")]
        public async Task<ActionResult<API.Models.Map>> MapUpdateRequest(API.Models.Map mapItem)
        {
            return mapItem;
        }

        [HttpGet("MapGetResponse")]
        public async Task<ActionResult<API.Models.Map>> MapGetResponse(Guid id)
        {
            return new API.Models.Map(id);
        }

        [HttpPost("MapImageUploadRequest"), DisableRequestSizeLimit]
        public async Task<ActionResult<Guid>> MapImageUploadRequest()
        {
            var file = Request.Form.Files[0];
            var fileId = Guid.NewGuid();
           
            return fileId;
        }

        [HttpGet("MapImageIdGetResponse")]
        public async Task<ActionResult<API.Models.Image>> MapImageIdGetResponse(Guid id)
        {
            return new API.Models.Image(id, new Byte[0]);
        }

    }
}
