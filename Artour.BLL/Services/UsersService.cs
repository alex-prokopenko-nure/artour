using Artour.BLL.Models;
using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Artour.Domain.EntityFramework.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Artour.BLL.Helper;
using Artour.Domain.Models;
using Artour.Domain.Enums;

namespace Artour.BLL.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly JwtSettings jwtSettings;
        private readonly Random _rnd;
        private const Int32 YEAR_IN_MINUTES = 525600;

        public UsersService(ApplicationDbContext applicationDbContext, IMapper mapper, IConfiguration configuration)
            : base(applicationDbContext, mapper, configuration)
        {
            _rnd = new Random();
            jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
        }

        private String BuildToken(Int32 userId, Boolean remember)
        {
            var key = new SymmetricSecurityKey(Convert.FromBase64String(jwtSettings.IssuerSigningKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jti = _rnd.Next().ToString("X08");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            };

            var token = new JwtSecurityToken(
                jwtSettings.ValidIssuer,
                jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(remember ? YEAR_IN_MINUTES : jwtSettings.TokenLifeTime),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var result = await _applicationDbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserViewModel>>(result);
        }

        public async Task<UserViewModel> GetUserById(Int32 userId)
        {
            var result = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            return _mapper.Map<UserViewModel>(result);
        }

        public async Task<(UserViewModel user, string token)> GetUserInfoByEmail(String email)
        {
            var result = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (result == null)
            {
                return (null, null);
            }
            var token = BuildToken(result.UserId, false);
            return (_mapper.Map<UserViewModel>(result), token);
        }

        public async Task<String> LoginUser(LoginViewModel login)
        {
            var passwordHash = CryptoHelper.GetMD5Hash(login.Password);
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x =>
                    (x.Email == login.Login || x.Username == login.Login) &&
                    x.Password == passwordHash);

            if (user == null)
            {
                throw new ArgumentException();
            }

            var token = BuildToken(user.UserId, login.Remember);

            return token;
        }

        public async Task RegisterUser(RegisterViewModel newUser)
        {
            var duplicateUser = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == newUser.Email || x.Username == newUser.Username);

            if (duplicateUser != null)
            {
                throw new ArgumentException();
            }

            User user =
                new User
                {
                    Username = newUser.Username,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Email = newUser.Email,
                    ProfileType = ProfileType.Common,
                    DateOfBirth = newUser.DateOfBirth,
                    Password = CryptoHelper.GetMD5Hash(newUser.Password)
                };

            await _applicationDbContext.Users.AddAsync(user);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task ChangePassword(int userId, ChangePasswordViewModel passwordViewModel)
        {
            var userToUpdate = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userToUpdate.Password == CryptoHelper.GetMD5Hash(passwordViewModel.OldPassword))
            {
                userToUpdate.Password = CryptoHelper.GetMD5Hash(passwordViewModel.NewPassword);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public async Task UpdateUserInfo(int userId, UserViewModel userViewModel)
        {
            var userToUpdate = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            userToUpdate.DateOfBirth = userViewModel.DateOfBirth;
            userToUpdate.FirstName = userViewModel.FirstName;
            userToUpdate.LastName = userViewModel.LastName;
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
