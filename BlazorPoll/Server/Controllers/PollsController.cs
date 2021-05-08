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

        public PollsController(IPollsService pollsService)
        {
            _pollsService = pollsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPollById(Guid id)
        {
            var poll = await _pollsService.FindById(id);
            Console.WriteLine(poll);
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
            Console.WriteLine("Create");
            Thread.Sleep(1000);

            poll = await _pollsService.Create(poll);

            return CreatedAtAction(nameof(GetPollById), new {id = poll.Id}, poll);
        }

        //[HttpPut("{pollId}/answer/single")]
        //public async Task<IActionResult> AnswerPoll(Guid pollId, Answer answer)
        //{
        //    Thread.Sleep(1000);

        //    var poll = InMemoryPolls.First(p => p.Id == pollId);
            
        //    poll.Answers.First(a => a.Id == answer.Id).Count++;

        //    return Ok();
        //}
    }
}
