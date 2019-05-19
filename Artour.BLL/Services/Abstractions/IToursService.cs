using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IToursService
    {
        Task<IEnumerable<TourViewModel>> GetAllTours();
        Task<IEnumerable<LightTourViewModel>> GetAllToursLight();
        Task<TourViewModel> GetTour(Int32 tourId);
        Task<IEnumerable<TourViewModel>> GetUsersTours(Int32 userId);
        Task<TourStatisticsViewModel> GetTourStatistics(Int32 tourId);
        Task<TourViewModel> CreateTour(TourViewModel tour);
        Task<TourViewModel> UpdateTour(Int32 tourId, TourViewModel tour);
        Task DeleteTour(Int32 tourId);
    }
}
