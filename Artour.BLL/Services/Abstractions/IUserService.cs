using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();
    }
}
