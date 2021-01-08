using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface IPollService
    {
        Task<Guid> AddPoll(Poll poll);
        Task<Poll> GetPollById(Guid id);
    }
}
