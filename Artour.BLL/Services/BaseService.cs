using Artour.Domain.EntityFramework.Context;
using AutoMapper;
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

        public BaseService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }
    }
}
