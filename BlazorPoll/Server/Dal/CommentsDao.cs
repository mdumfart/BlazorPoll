using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Dal
{
    public class CommentsDao : ICommentsDao
    {
        private readonly ApplicationDbContext _context;

        public CommentsDao(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Comment> Create(Comment comment)
        {
            _context.Entry(comment.Poll).State = EntityState.Unchanged;
            _context.Entry(comment.Author).State = EntityState.Unchanged;

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> FindByUsername(string username)
        {
            return await _context.Comments.Where(c => c.Author.Username == username).Include(c => c.Poll).OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<List<Comment>> FindByPollId(Guid pollId)
        {
            return await _context.Comments.Where(c => c.Poll.Id == pollId).ToListAsync();
        }
    }
}
