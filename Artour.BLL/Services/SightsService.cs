using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using Artour.Domain.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services
{
    public class SightsService : BaseService, ISightsService
    {
        public SightsService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
            : base(applicationDbContext, mapper, configuration)
        { }

        public async Task<SightViewModel> CreateSight(SightViewModel sight)
        {
            Sight sightToCreate = 
                new Sight
                {
                    Description = sight.Description,
                    Title = sight.Title,
                    TourId = sight.TourId
                };
            sightToCreate.Tour = await _applicationDbContext.Tours.FirstOrDefaultAsync(x => x.TourId == sight.TourId);
            await _applicationDbContext.Sights.AddAsync(sightToCreate);
            await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<SightViewModel>(sightToCreate);
        }

        public async Task DeleteSight(int sightId)
        {
            Sight sightToDelete = new Sight { SightId = sightId };
            _applicationDbContext.Sights.Attach(sightToDelete);
            _applicationDbContext.Sights.Remove(sightToDelete);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SightViewModel>> GetTourSights(int tourId)
        {
            var result = await _applicationDbContext.Sights.Include(x => x.Images).Where(x => x.TourId == tourId).ToListAsync();
            return _mapper.Map<IEnumerable<SightViewModel>>(result);
        }

        public async Task<SightViewModel> UpdateSight(int sightId, SightViewModel sight)
        {
            var sightToUpdate = await _applicationDbContext.Sights.FirstOrDefaultAsync(x => x.SightId == sightId);
            sightToUpdate.Description = sight.Description;
            sightToUpdate.Title = sight.Title;
            _applicationDbContext.Sights.Update(sightToUpdate);
            await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<SightViewModel>(sightToUpdate);
        }
    }
}
