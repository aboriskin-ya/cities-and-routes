using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [Route("mapimage")]
    [ApiController]
    public class MapImageController : ControllerBase
    {
        private readonly IImageService _service;

        public MapImageController(IImageService service)
        {
            _service = service;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var contentType = file.ContentType;

                    byte[] bytes = null;
                    using (var str = new MemoryStream())
                    {
                        await file.CopyToAsync(str);
                        bytes = str.ToArray();
                    }


                    _service.CreateUpdate(new MapImage()
                    {
                        Data = bytes,
                        ContentType = contentType
                    });

                    return Ok($"successful adding {fileName}");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetImage(int id)
        {
            MapImage img = _service.GetImage(id);
            if (img == null)
                return NotFound();

            return File(img.Data, img.ContentType);
        }

    }
}
