﻿using Artour.BLL.Services.Abstractions;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Services
{
    public class ToursService : BaseService, IToursService
    {
        public ToursService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
            : base(applicationDbContext, mapper, configuration)
        { }
    }
}
