using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
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

        [HttpGet("{sightId}")]
        public async Task<ActionResult<SightViewModel>> GetSightById(Int32 sightId)
        {
            var result = await _sightsService.GetSight(sightId);
            return Ok(result);
        }

        [HttpGet("tour/{tourId}")]
        public async Task<ActionResult<IEnumerable<SightViewModel>>> GetSightsByTourId(Int32 tourId)
        {
            var result = await _sightsService.GetTourSights(tourId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<SightViewModel>> CreateSight([FromBody]SightViewModel sight)
        {
            var result = await _sightsService.CreateSight(sight);
            return Ok(result);
        }

        [HttpPut("{sightId}")]
        public async Task<ActionResult<SightViewModel>> UpdateSight(Int32 sightId, [FromBody]SightViewModel sight)
        {
            var result = await _sightsService.UpdateSight(sightId, sight);
            return Ok(result);
        }

        [HttpDelete("{sightId}")]
        public async Task<ActionResult> DeleteSight(Int32 sightId)
        {
            await _sightsService.DeleteSight(sightId);
            return Ok();
        }
    }
}
