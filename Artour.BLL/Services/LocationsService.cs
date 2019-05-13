using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services
{
    public class LocationsService : BaseService, ILocationsService
    {
        public LocationsService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IConfiguration configuration,
            IMemoryCache cache) 
            : base(applicationDbContext, mapper, configuration, cache)
        { }

        public async Task<IEnumerable<CityViewModel>> GetAllCities()
        {
            var result = await _applicationDbContext.Cities.ToListAsync();
            return _mapper.Map<IEnumerable<CityViewModel>>(result);
        }

        public async Task<IEnumerable<CountryViewModel>> GetAllCountries()
        {
            var result = await _applicationDbContext.Countries.ToListAsync();
            return _mapper.Map<IEnumerable<CountryViewModel>>(result);
        }

        public async Task<IEnumerable<RegionViewModel>> GetAllRegions()
        {
            var result = await _applicationDbContext.Regions.ToListAsync();
            return _mapper.Map<IEnumerable<RegionViewModel>>(result);
        }
    }
}
