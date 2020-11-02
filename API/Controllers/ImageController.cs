using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Services.Interfaces;

namespace API.Controllers
{
    [Route("image")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;

        public ImageController(IImageService service)
        {
            _service = service;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<ActionResult<Guid>> UploadImage()
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

                    var img = new Image()
                    {
                        Data = bytes,
                        ContentType = contentType
                    };
                    _service.StoreImage(img);

                    return img.Id;
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
        [Route("{id:Guid}")]
        public IActionResult GetImage(Guid id)
        {
            Image img = _service.GetImage(id);
            if (img == null)
                return NotFound();

            return File(img.Data, img.ContentType);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetImage()
        {
            IEnumerable<Image> ImageList = _service.GetImage();

            if (ImageList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(ImageList);
        }

    }
}
