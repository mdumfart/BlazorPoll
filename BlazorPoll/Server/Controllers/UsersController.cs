using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Authentication;

namespace BlazorPoll.Server.Controllers
{
    [Route("api/users")]
    [Controller]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserCredentialsDto userCredentials)
        {
            return await _userService.Register(userCredentials);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] UserCredentialsDto userCredentials)
        {
            return await _userService.Login(userCredentials, HttpContext);
        }

        [HttpGet("current")]
        public async Task<ActionResult<User>> GetCurrent()
        {
            User currentUser = new User();

            if (User.Identity.IsAuthenticated)
            {
                currentUser.Username = User.FindFirstValue(ClaimTypes.Name);
            }

            return await Task.FromResult(currentUser);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

    }
}
