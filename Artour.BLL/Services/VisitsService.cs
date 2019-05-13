using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using Artour.Domain.Models;
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
    public class VisitsService : BaseService, IVisitsService
    {
        public VisitsService(
            ApplicationDbContext applicationDbContext, 
            IMapper mapper, 
            IConfiguration configuration,
            IMemoryCache cache)
            : base(applicationDbContext, mapper, configuration, cache)
        { }

        public async Task<VisitViewModel> GetVisit(Guid visitId)
        {
            var result = await _applicationDbContext.Visits
                .Include(x => x.Tour)
                .ThenInclude(x => x.Sights)
                .ThenInclude(x => x.Images)
                .Include(x => x.User)
                .Include(x => x.SightSeens)
                .FirstOrDefaultAsync(x => x.VisitId == visitId);

            result.Tour.Visits = null;

            return _mapper.Map<VisitViewModel>(result);
        }

        public async Task<Guid> StartVisit(VisitViewModel visit)
        {
            Guid visitGuid = Guid.NewGuid();
            Visit visitToAdd =
                new Visit
                {
                    VisitId = visitGuid,
                    TourId = visit.TourId,
                    UserId = visit.UserId,
                    StartDate = visit.StartDate
                };
            visitToAdd.Tour = await _applicationDbContext.Tours.FirstOrDefaultAsync(x => x.TourId == visit.TourId);
            visitToAdd.User = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == visit.UserId);

            await _applicationDbContext.Visits.AddAsync(visitToAdd);
            await _applicationDbContext.SaveChangesAsync();
            return visitGuid;
        }

        public async Task FinishVisit(Guid visitId)
        {
            Visit visitToUpdate = await _applicationDbContext.Visits.FirstOrDefaultAsync(x => x.VisitId == visitId);
            visitToUpdate.EndDate = DateTimeOffset.Now;
            _applicationDbContext.Visits.Update(visitToUpdate);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
