using Artour.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/sight-images")]
    [ApiController]
    public class SightImagesController : ControllerBase
    {
        private readonly ISightImagesService _sightImagesService;

        public SightImagesController(ISightImagesService sightImagesService)
        {
            _sightImagesService = sightImagesService;
        }
    }
}
