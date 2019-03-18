using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(ApplicationDbContext applicationDbContext, IMapper mapper)
            : base(applicationDbContext, mapper)
        {}

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var result = await _applicationDbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(result);
        }
    }
}
