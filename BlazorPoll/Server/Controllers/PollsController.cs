using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Controllers
{
    [Route("api/polls")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private static readonly List<Poll> InMemoryPolls = new List<Poll>();

        public PollsController()
        {
            InMemoryPolls.Add(new Poll()
            {
                Question = "this is a question"
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPollById(Guid id)
        {
            var poll = InMemoryPolls.First(p => p.Id == id);

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
            Thread.Sleep(2000);
            
            poll.Id = Guid.NewGuid();
            InMemoryPolls.Add(poll);

            return CreatedAtAction(nameof(GetPollById), new {id = poll.Id}, poll);
        }
    }
}
