using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        [HttpGet("cities")]
        public async Task<ActionResult<IEnumerable<CityViewModel>>> GetAllCities()
        {
            var result = await _locationsService.GetAllCities();
            return Ok(result);
        }

        [HttpGet("countries")]
        public async Task<ActionResult<IEnumerable<CountryViewModel>>> GetAllCountries()
        {
            var result = await _locationsService.GetAllCountries();
            return Ok(result);
        }

        [HttpGet("regions")]
        public async Task<ActionResult<IEnumerable<RegionViewModel>>> GetAllRegions()
        {
            var result = await _locationsService.GetAllRegions();
            return Ok(result);
        }
    }
}
