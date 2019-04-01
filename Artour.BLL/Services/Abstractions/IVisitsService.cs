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
        Task<Guid> StartVisit(VisitViewModel visit);
        Task FinishVisit(Guid visitId);
    }
}
