using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly IAnswerDao _answerDao;

        public AnswersService(IAnswerDao answerDao)
        {
            _answerDao = answerDao;
        }
        
        public async Task<Answer> SubmitVote(Answer answer)
        {
            answer.Count++;
            
            return await _answerDao.Update(answer);
        }

        public async Task<Answer> FindByPollIdAndAnswerId(Guid pollId, int answerId)
        {
            return await _answerDao.FindByPollIdAndAnswerId(pollId, answerId);
        }
    }
}
