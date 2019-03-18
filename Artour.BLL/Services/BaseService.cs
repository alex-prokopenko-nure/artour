using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artour.BLL.Services
{
    public class BaseService
    {
        protected ApplicationDbContext _applicationDbContext;
        protected IMapper _mapper;

        public BaseService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
        }
    }
}
