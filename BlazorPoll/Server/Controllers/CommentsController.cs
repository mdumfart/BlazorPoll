using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Controllers
{
    [Route("api/comments")]
    [Controller]
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Create([FromBody] Comment comment)
        {
            return Ok(await _commentsService.Create(comment));
        }
    }
}
