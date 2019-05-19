using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{visitId}")]
        public async Task<ActionResult<VisitViewModel>> GetVisit(Guid visitId)
        {
            var result = await _visitsService.GetVisit(visitId);
            return Ok(result);
        }


        [HttpPost("start")]
        [Authorize]
        public async Task<ActionResult<Guid>> StartVisit(Int32 tourId, Int32 userId)
        {
            var result = await _visitsService.StartVisit(tourId, userId);
            return Ok(result);
        }

        [HttpPost("{visitId}/finish")]
        [Authorize]
        public async Task<ActionResult> FinishVisit(Guid visitId)
        {
            await _visitsService.FinishVisit(visitId);
            return Ok();
        }
    }
}
