using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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

        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> UploadImage()
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
                return Ok(img.Id);
            }
            else
            {
                return BadRequest();
            }
        }

        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetImage(Guid id)
        {
            Image img = _service.GetImage(id);
            if (img == null)
                return NotFound();

            return File(img.Data, img.ContentType);
        }

        [ProducesResponseType(typeof(IEnumerable<Image>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Route("getall")]
        public IActionResult GetImage()
        {
            IEnumerable<Image> ImageList = _service.GetImages();

            if (ImageList.Count() == 0)
            {
                return NotFound();
            }

            return Ok(ImageList);
        }

    }
}
