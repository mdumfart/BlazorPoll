using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Controllers
{
    [Route("api/polls")]
    [ApiController]
    public class PollsController : ControllerBase
    {
        private readonly List<Poll> inMemoryPolls;

        public PollsController()
        {
            inMemoryPolls = new List<Poll>();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Poll>> GetPollById(int id)
        {
            var poll = inMemoryPolls.First(p => p.Id == id);

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
            poll.Id = inMemoryPolls.Count;
            inMemoryPolls.Add(poll);

            return CreatedAtAction(nameof(GetPollById), new {id = poll.Id}, poll);
        }
    }
}
