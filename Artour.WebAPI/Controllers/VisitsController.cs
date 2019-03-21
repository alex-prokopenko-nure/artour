using Artour.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/visits")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly IVisitsService _visitsService;

        public VisitsController(IVisitsService visitsService)
        {
            _visitsService = visitsService;
        }
    }
}
