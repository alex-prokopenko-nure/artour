using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IVisitsService
    {
        Task<VisitViewModel> GetVisit(Guid visitId);
        Task<Guid> StartVisit(Int32 tourId, Int32 userId);
        Task FinishVisit(Guid visitId);
    }
}
