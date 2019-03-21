using Artour.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/sights")]
    [ApiController]
    public class SightsController : ControllerBase
    {
        private readonly ISightsService _sightsService;

        public SightsController(ISightsService sightsService)
        {
            _sightsService = sightsService;
        }
    }
}
