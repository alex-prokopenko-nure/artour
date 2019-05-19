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
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Artour.BLL.Services
{
    public class UsersService : BaseService, IUsersService
    {
        private readonly JwtSettings jwtSettings;
        private readonly Random _rnd;
        private const Int32 YEAR_IN_MINUTES = 525600;

        public UsersService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper, 
            IConfiguration configuration,
            IMemoryCache cache)
            : base(applicationDbContext, mapper, configuration, cache)
        {
            _rnd = new Random();
            jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
        }

        public Int32 ParseJwtToken(String jwtToken)
        {
            try
            {
                var jwtHandler = new JwtSecurityTokenHandler();
                var token = jwtHandler.ReadToken(jwtToken) as JwtSecurityToken;
                var userIdClaim = token.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub);

                if (userIdClaim == null)
                {
                    throw new InvalidOperationException("Incorrect token");
                }

                if (!Int32.TryParse(userIdClaim.Value, out Int32 userId))
                {
                    throw new InvalidOperationException("Incorrect token");
                }

                return userId;
            }
            catch (SecurityTokenInvalidLifetimeException)
            {
                throw new InvalidOperationException("Token expired");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                throw new InvalidOperationException("Invalid token key");
            }
        }

        public String BuildToken(Int32 userId, Boolean remember)
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

        public async Task ResetAndUpdateUserPassword(int id, String password)
        {
            try
            {
                var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserId == id);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Password = CryptoHelper.GetMD5Hash(password);

                await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
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

        public async Task<UserStatisticsViewModel> GetUserStatistics(int userId)
        {
            var usersVisits = await _applicationDbContext.Visits
                .Include(x => x.SightSeens)
                .Include(x => x.Tour)
                .ThenInclude(x => x.City)
                .ThenInclude(x => x.Country)
                .ThenInclude(x => x.Region)
                .Where(x => x.UserId == userId && x.EndDate != null).ToListAsync();
            var distinctTourVisits = usersVisits.Distinct(new DistinctToursComparer());
            var toursVisited = distinctTourVisits.Count();
            var sightsSeen = distinctTourVisits.Aggregate(0, (sum, x) => sum += x.SightSeens != null ? x.SightSeens.Count() : 0, sum => sum);
            var visitsInfo = usersVisits.Select(x => 
            new VisitInfoViewModel {
                VisitId = x.VisitId,
                DurationInSeconds = (int)(x.EndDate - x.StartDate).TotalSeconds,
                TourId = x.TourId,
                TourTitle = x.Tour.Title,
                City = x.Tour.City.Name,
                Country = x.Tour.City.Country.Name,
                Region = x.Tour.City.Country.Region.Name
            }).ToList();
            UserStatisticsViewModel result = new UserStatisticsViewModel { ToursVisited = toursVisited, SightsSeen = sightsSeen, Visits = visitsInfo };

            return result;
        }
    }
}
