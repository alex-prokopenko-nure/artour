using Artour.BLL.Services.Abstractions;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Services
{
    public class SightImagesService : BaseService, ISightImagesService
    {
        public SightImagesService(ApplicationDbContext applicationDbContext, IMapper mapper)
            : base(applicationDbContext, mapper)
        { }
    }
}
