using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Exceptions;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                return Ok(await _userService.Register(userCredentials));
            }
            catch (UserAlreadyExistsException e)
            {
                return Ok(new RegisterResultDto() { Successful = false, Errors = new List<string>() { e.Message } });
            }
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

        [HttpGet("{username}")]
        public async Task<IActionResult> GetByUserName(string username)
        {
            var user = await _userService.FindByUserName(username);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound(new ProblemDetails()
            {
                Title = "User not found",
                Detail = $"User with username [{username}] not found"
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpGet("{username}/comments/{page}"), Authorize]
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

        [HttpGet("{username}/polls"), Authorize]
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
