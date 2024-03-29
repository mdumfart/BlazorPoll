﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface IPollService
    {
        Task<Guid> AddPoll(Poll poll);
        Task<Poll> FindPollById(Guid id);
        Task<List<Poll>> FindByUsername(string username);
        Task<bool> SendSinglePollAnswer(Poll poll, Answer answer, IPollHubService pollHubService);
        Task<bool> SendMultiplePollAnswers(Poll poll, List<Answer> answers, IPollHubService pollHubService);
    }
}
