using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourViewModel>>> GetAllTours()
        {
            var result = await _toursService.GetAllTours();
            return Ok(result);
        }

        [HttpGet("light")]
        public async Task<ActionResult<IEnumerable<LightTourViewModel>>> GetAllToursLight()
        {
            var result = await _toursService.GetAllToursLight();
            return Ok(result);
        }

        [HttpGet("{tourId}")]
        public async Task<ActionResult<TourViewModel>> GetTour(Int32 tourId)
        {
            var result = await _toursService.GetTour(tourId);
            return Ok(result);
        }

        [HttpGet("{tourId}/statistics")]
        public async Task<ActionResult<TourStatisticsViewModel>> GetTourStatistics(Int32 tourId)
        {
            var result = await _toursService.GetTourStatistics(tourId);
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TourViewModel>>> GetUsersTours(Int32 userId)
        {
            var result = await _toursService.GetUsersTours(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TourViewModel>> CreateTour([FromBody]TourViewModel tour)
        {
            var result = await _toursService.CreateTour(tour);
            return Ok(result);
        }

        [HttpPut("{tourId}")]
        public async Task<ActionResult<TourViewModel>> UpdateTour(Int32 tourId, [FromBody]TourViewModel tour)
        {
            var result = await _toursService.UpdateTour(tourId, tour);
            return Ok(result);
        }

        [HttpDelete("{tourId}")]
        public async Task<IActionResult> DeleteTour(Int32 tourId)
        {
            await _toursService.DeleteTour(tourId);
            return Ok();
        }
    }
}
