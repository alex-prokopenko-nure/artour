using Artour.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/tours")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly IToursService _toursService;

        public ToursController(IToursService toursService)
        {
            _toursService = toursService;
        }
    }
}
