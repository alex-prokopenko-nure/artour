using Artour.BLL.Services.Abstractions;
using Artour.BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artour.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMailSender _mailSender;

        public UsersController(IUsersService usersService, IMailSender mailSender)
        {
            _usersService = usersService;
            _mailSender = mailSender;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> GetUsers()
        {
            var result = await _usersService.GetAllUsers();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserViewModel>> GetUser(Int32 userId)
        {
            var result = await _usersService.GetUserById(userId);
            return result;
        }

        [HttpPost("login")]
        public async Task<ActionResult<String>> LoginUser([FromBody]LoginViewModel login)
        {
            try
            {
                var result = await _usersService.LoginUser(login);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody]RegisterViewModel newUser)
        {
            try
            {
                await _usersService.RegisterUser(newUser);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("send-confirmation-email")]
        public async Task<ActionResult> SendConfirmationEmail([FromBody]String email)
        {
            (UserViewModel user, string token) = await _usersService.GetUserInfoByEmail(email);

            if (user == null || token == null)
            {
                return BadRequest();
            }

            await _mailSender.SendConfirmationEmail(user, token);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{userId}/change-password")]
        public async Task<ActionResult<UserViewModel>> ChangePassword(Int32 userId, [FromBody]ChangePasswordViewModel passwordViewModel)
        {
            try
            {
                await _usersService.ChangePassword(userId, passwordViewModel);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserViewModel>> UpdateUser(Int32 userId, [FromBody]UserViewModel userViewModel)
        {
            await _usersService.UpdateUserInfo(userId, userViewModel);
            return Ok();
        }
    }
}
