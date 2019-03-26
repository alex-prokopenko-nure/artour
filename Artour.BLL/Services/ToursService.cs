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
    public class ToursService : BaseService, IToursService
    {
        public ToursService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
            : base(applicationDbContext, mapper, configuration)
        { }

        public async Task<TourViewModel> CreateTour(TourViewModel tour)
        {
            Tour tourToCreate = _mapper.Map<Tour>(tour);
            await _applicationDbContext.Tours.AddAsync(tourToCreate);
            await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<TourViewModel>(tourToCreate);
        }

        public async Task DeleteTour(int tourId)
        {
            var deletedTour = new Tour { TourId = tourId };
            _applicationDbContext.Tours.Attach(deletedTour);
            _applicationDbContext.Tours.Remove(deletedTour);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TourViewModel>> GetAllTours()
        {
            var result = await _applicationDbContext.Tours
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TourViewModel>>(result);
        }

        public async Task<TourViewModel> GetTour(int tourId)
        {
            var result = await _applicationDbContext.Tours
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.TourId == tourId);

            return _mapper.Map<TourViewModel>(result);
        }

        public async Task<IEnumerable<TourViewModel>> GetUsersTours(Int32 userId)
        {
            var result = await _applicationDbContext.Tours
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .Where(x => x.OwnerId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TourViewModel>>(result);
        }

        public async Task<TourViewModel> UpdateTour(int tourId, TourViewModel tour)
        {
            var tourToUpdate = await _applicationDbContext.Tours.FirstOrDefaultAsync(x => x.TourId == tourId);
            tourToUpdate.Title = tour.Title;
            tourToUpdate.Description = tour.Description;
            _applicationDbContext.Tours.Update(tourToUpdate);
            await _applicationDbContext.SaveChangesAsync();
            return _mapper.Map<TourViewModel>(tourToUpdate);
        }
    }
}
