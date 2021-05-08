using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorPoll.Server.Services;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Controllers
{
    [Route("api/polls")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly IPollsService _pollsService;
        private readonly IAnswersService _answersService;

        public PollsController(IPollsService pollsService, IAnswersService answersService)
        {
            _pollsService = pollsService;
            _answersService = answersService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPollById(Guid id)
        {
            var poll = await _pollsService.FindById(id);
            if (poll == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "Poll not found",
                    Detail = $"Poll with id [{id}] not found"
                });

            return Ok(poll);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePoll(Poll poll)
        {
            Thread.Sleep(1000);

            poll = await _pollsService.Create(poll);

            return CreatedAtAction(nameof(GetPollById), new {id = poll.Id}, poll);
        }

        [HttpPut("{pollId}/vote-single/{answerId}")]
        public async Task<IActionResult> VoteOnSinglePoll(Guid pollId, int answerId)
        {
            var poll = await _pollsService.FindById(pollId);

            if (poll == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "Poll not found",
                    Detail = $"Poll with id [{pollId}] not found"
                });

            var answer = await _answersService.FindByPollIdAndAnswerId(pollId, answerId);

            if (answer == null)
                return NotFound(new ProblemDetails()
                {
                    Title = "Answer not found",
                    Detail = $"Answer with id [{answerId}] not found"
                });

            return Ok(await _answersService.SubmitVote(answer));
        }
    }
}
