using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface ISightsService
    {
        Task<IEnumerable<SightViewModel>> GetTourSights(Int32 tourId);
        Task<SightViewModel> CreateSight(SightViewModel sight);
        Task<SightViewModel> GetSight(int sightId);
        Task<SightViewModel> UpdateSight(Int32 sightId, SightViewModel sight);
        Task DeleteSight(Int32 sightId);
    }
}
