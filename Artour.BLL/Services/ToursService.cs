using Artour.BLL.Helper;
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
                .Include(x => x.City)
                .Include(x => x.Comments)
                .Include(x => x.Visits)
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .ToListAsync();

            foreach (var tour in result)
            {
                tour.City.Tours = null;
                foreach (var visit in tour.Visits)
                {
                    visit.Tour = null;
                }
            }

            return _mapper.Map<IEnumerable<TourViewModel>>(result);
        }

        public async Task<TourViewModel> GetTour(int tourId)
        {
            var result = await _applicationDbContext.Tours
                .Include(x => x.City)
                .Include(x => x.Comments)
                .Include(x => x.Visits)
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .FirstOrDefaultAsync(x => x.TourId == tourId);

            result.City.Tours = null;
            foreach (var visit in result.Visits)
            {
                visit.Tour = null;
            }

            return _mapper.Map<TourViewModel>(result);
        }

        public async Task<IEnumerable<TourViewModel>> GetUsersTours(Int32 userId)
        {
            var result = await _applicationDbContext.Tours
                .Include(x => x.City)
                .Include(x => x.Comments)
                .Include(x => x.Visits)
                .Include(x => x.Sights)
                .ThenInclude(x => x.Images)
                .Where(x => x.OwnerId == userId)
                .ToListAsync();

            foreach (var tour in result)
            {
                tour.City.Tours = null;
                foreach (var visit in tour.Visits)
                {
                    visit.Tour = null;
                }
            }

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

        public async Task<TourStatisticsViewModel> GetTourStatistics(int tourId)
        {
            var visits = await _applicationDbContext.Visits.Where(x => x.TourId == tourId && x.EndDate != null).ToListAsync();
            var comments = await _applicationDbContext.Comments.Where(x => x.TourId == tourId).ToListAsync();
            var visitsNumber = visits.Count();
            var usersVisited = visits.Distinct(new UsersVisitComparer()).Count();
            var visitsLastWeek = visits.Where(x => (DateTimeOffset.Now - x.StartDate).TotalDays < 7).Count();
            var averageTourTime = visitsNumber != 0 ? visits.Average(x => (x.EndDate - x.StartDate).TotalMilliseconds) : 0;
            var commentsNumber = comments.Count();
            var averageMark = commentsNumber != 0 ? comments.Average(x => x.Mark) : 0;

            return new TourStatisticsViewModel
            { VisitsNumber = visitsNumber, AverageMark = averageMark, AverageTourTime = averageTourTime, CommentsNumber = commentsNumber, UsersVisited = usersVisited, VisitsLastWeek = visitsLastWeek };
        }
    }
}
