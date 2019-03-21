using Artour.BLL.Services.Abstractions;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Services
{
    public class SightsService : BaseService, ISightsService
    {
        public SightsService(ApplicationDbContext applicationDbContext, IMapper mapper)
            : base(applicationDbContext, mapper)
        { }
    }
}
