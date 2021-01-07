using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface IPollService
    {
        Task<bool> AddPoll(Poll poll);
        Task<Poll> GetPollById(int id);
    }
}
