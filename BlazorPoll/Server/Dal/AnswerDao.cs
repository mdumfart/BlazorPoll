using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Dal
{
    public class AnswerDao : IAnswerDao
    {
        private readonly ApplicationDbContext _context;

        public AnswerDao(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Answer> Update(Answer answer)
        {
            _context.Entry(answer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task<Answer> FindByPollIdAndAnswerId(Guid pollId, int answerId)
        {
            return await _context.Answers.Include(a => a.Poll).FirstOrDefaultAsync(a => a.Id == answerId && a.Poll.Id == pollId);
        }
    }
}
