using Artour.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Artour.BLL.Services.Abstractions
{
    public interface IUsersService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsers();
        Task<UserViewModel> GetUserById(Int32 userId);
        Task<String> LoginUser(LoginViewModel login);
        Task RegisterUser(RegisterViewModel newUser);
        Task ChangePassword(Int32 userId, ChangePasswordViewModel passwordViewModel);
        Task UpdateUserInfo(Int32 userId, UserViewModel userViewModel);
        Task<(UserViewModel user, string token)> GetUserInfoByEmail(String email);
    }
}
