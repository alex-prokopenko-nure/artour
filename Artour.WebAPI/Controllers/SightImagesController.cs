using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/sight-images")]
    public class SightImagesController : ControllerBase
    {
        private readonly ISightImagesService _sightImagesService;

        public SightImagesController(ISightImagesService sightImagesService)
        {
            _sightImagesService = sightImagesService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SightImageViewModel>> GetImageById(Int32 id)
        {
            var result = await _sightImagesService.GetImageByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}/data")]
        public async Task<ActionResult> GetImageDataById(Int32 id)
        {
            var result = await _sightImagesService.GetImageByIdAsync(id);
            var fileInfo = _sightImagesService.GetFileInfo(result);
            var mimeType = _sightImagesService.GetImageMimeType(fileInfo.Extension);

            return File(fileInfo.OpenRead(), mimeType, fileInfo.Name);
        }

        [HttpPost("{sightId}")]
        public async Task<ActionResult<SightImageViewModel>> CreateSightImage([FromForm]IFormFile fileToSave, [FromForm]String description, Int32 sightId)
        {
            var result = await _sightImagesService.CreateImage(fileToSave, description, sightId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(Int32 id, [FromForm]IFormFile fileToSave)
        {
            await _sightImagesService.UpdateImage(id, fileToSave);
            return Ok();
        }

        [HttpPut("{id}/info")]
        public async Task<IActionResult> UpdateInfo(Int32 id, [FromBody]SightImageViewModel model)
        {
            await _sightImagesService.UpdateInfo(id, model);
            return Ok();
        }

        [HttpPut("change-order")]
        public async Task<IActionResult> ChangeOrder(Int32 previousOrder, Int32 currentOrder)
        {
            await _sightImagesService.ChangeOrder(previousOrder, currentOrder);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(Int32 id)
        {
            await _sightImagesService.DeleteImage(id);
            return Ok();
        }
    }
}
