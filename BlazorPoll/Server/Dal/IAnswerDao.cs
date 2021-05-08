using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Dal
{
    public interface IAnswerDao
    {
        public Task<Answer> Update(Answer answer);
        public Task<Answer> FindByPollIdAndAnswerId(Guid pollId, int answerId);

    }
}
