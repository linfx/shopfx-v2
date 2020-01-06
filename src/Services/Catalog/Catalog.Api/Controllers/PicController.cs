﻿using System.IO;
using System.Net;
using System.Threading.Tasks;
using Catalog.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Controllers
{
    public class PicController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly CatalogContext _catalogContext;

        public PicController(IHostingEnvironment env,
            CatalogContext catalogContext)
        {
            _env = env;
            _catalogContext = catalogContext;
        }

        [HttpGet]
        [Route("api/v1/catalog/items/{catalogItemId:int}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetImage(int catalogItemId)
        {
            if (catalogItemId <= 0)
                return BadRequest();

            var item = await _catalogContext.CatalogItems
                .SingleOrDefaultAsync(ci => ci.Id == catalogItemId);

            if (item != null)
            {
                var webRoot = _env.WebRootPath;
                var path = Path.Combine(webRoot, item.PictureFileName);

                string imageFileExtension = Path.GetExtension(item.PictureFileName);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return File(buffer, mimetype);
            }

            return NotFound();
        }

        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    return MimeTypes.Image.Png;
                case ".gif":
                    return MimeTypes.Image.Gif;
                case ".jpg":
                case ".jpeg":
                    return MimeTypes.Image.Jpeg;
                case ".bmp":
                    return MimeTypes.Image.Bmp;
                case ".tiff":
                    mimetype = "image/tiff";
                    return MimeTypes.Image.Tiff;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    return MimeTypes.Application.OctetStream;
            }

            return mimetype;
        }
    }
}