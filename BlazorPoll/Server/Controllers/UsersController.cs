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
        private readonly ICommentsService _commentsService;
        private readonly IPollsService _pollsService;

        public UsersController(IUserService userService, ICommentsService commentsService, IPollsService pollsService)
        {
            _userService = userService;
            _commentsService = commentsService;
            _pollsService = pollsService;
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

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var username = User.FindFirstValue(ClaimTypes.Name);

                if (username != null)
                {
                    currentUser = await _userService.FindByUserName(username);
                }
            }

            return await Task.FromResult(currentUser);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpGet("{username}/comments")]
        public async Task<IActionResult> GetCommentsByUser(string username)
        {
            var user = await _userService.FindByUserName(username);

            if (user == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "User not found",
                    Detail = $"User with username [{username}] not found"
                });

            return Ok(await _commentsService.FindByUsername(username));
        }

        [HttpGet("{username}/polls")]
        public async Task<IActionResult> GetPollsByUser(string username)
        {
            var user = await _userService.FindByUserName(username);

            if (user == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "User not found",
                    Detail = $"User with username [{username}] not found"
                });

            return Ok(await _pollsService.FindByAuthorName(username));
        }
    }
}
