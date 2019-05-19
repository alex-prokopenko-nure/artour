using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using Artour.Domain.Models;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services
{
    public class SightSeensService : BaseService, ISightSeensService
    {
        public SightSeensService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IConfiguration configuration,
            IMemoryCache cache)
            : base(applicationDbContext, mapper, configuration, cache)
        { }

        public async Task CreateSightSeen(Int32 sightId, Guid visitId)
        {
            var sightSeenToAdd = new SightSeen{ SightId = sightId, VisitId = visitId, DateSeen = DateTimeOffset.Now};
            await _applicationDbContext.AddAsync(sightSeenToAdd);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
