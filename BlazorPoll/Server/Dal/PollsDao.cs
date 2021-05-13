using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Dal
{
    public class PollsDao : IPollsDao
    {
        private readonly ApplicationDbContext _context;

        public PollsDao(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Poll> Create(Poll poll)
        {
            if (poll.Author != null)
            {
                _context.Entry(poll.Author).State = EntityState.Unchanged;
            }

            await _context.Polls.AddAsync(poll);
            await _context.SaveChangesAsync();
            return poll;
        }

        public async Task<Poll> Update(Poll poll)
        {
            _context.Entry(poll).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return poll;
        }

        public async Task Delete(Poll poll)
        {
            var t = _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();
        }

        public async Task<Poll> FindById(Guid id)
        {
            return await _context.Polls
                .Include(p => p.Answers)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Author)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Poll>> FindAll()
        {
            return _context.Polls.ToList();
        }

        public async Task<List<Poll>> FindByAuthorName(string authorName)
        {
            return await _context.Polls
                .Where(p => p.Author.UserName == authorName)
                .Include(p => p.Answers)
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
