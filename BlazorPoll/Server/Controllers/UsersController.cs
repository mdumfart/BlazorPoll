using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Register([FromBody] UserCredentialsDto userCredentials)
        {
            return Ok(await _userService.Register(userCredentials));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto userCredentials)
        {
            var result = await _userService.Login(userCredentials, HttpContext);

            if (result.Successful)
            {
                return Ok(result);
            }

            return BadRequest(result);
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

        [HttpGet("{username}/comments/{page}")]
        public async Task<IActionResult> GetPaginatedCommentsByUser(string username, int page)
        {
            var user = await _userService.FindByUserName(username);

            if (user == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "User not found",
                    Detail = $"User with username [{username}] not found"
                });

            return Ok(await _commentsService.FindByUsernamePaginated(username, page));
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
