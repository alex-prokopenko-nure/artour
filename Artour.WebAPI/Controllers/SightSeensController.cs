using Artour.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/sight-seens")]
    [ApiController]
    public class SightSeensController : ControllerBase
    {
        private readonly ISightSeensService _sightSeensService;

        public SightSeensController(ISightSeensService sightSeensService)
        {
            _sightSeensService = sightSeensService;
        }
    }
}
