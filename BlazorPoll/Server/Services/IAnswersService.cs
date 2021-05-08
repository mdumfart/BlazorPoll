using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public interface IAnswersService
    {
        public Task<Answer> SubmitVote(Answer answer);
        public Task<Answer> FindByPollIdAndAnswerId(Guid pollId, int answerId);
    }
}
