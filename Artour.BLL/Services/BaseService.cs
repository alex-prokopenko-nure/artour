using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Services
{
    public class BaseService
    {
        protected ApplicationDbContext _applicationDbContext;
        protected IMapper _mapper;
        protected IConfiguration _configuration;
        protected IMemoryCache _cache;

        public BaseService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IConfiguration configuration, 
            IMemoryCache cache)
        {
            _configuration = configuration;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _cache = cache;
        }
    }
}
