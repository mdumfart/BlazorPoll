using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Dal
{
    public class CommentsDao : ICommentsDao
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 10;

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

        public async Task<PaginatedWrapperDto<List<Comment>>> FindByUsernamePaginated(string username, int page)
        {
            var skip = (page - 1) * PageSize;
            var pageCount = (double) _context.Comments.Count() / PageSize;

            var paginatedWrapper = new PaginatedWrapperDto<List<Comment>>
            {
                CurrentPage = page,
                PageCount = (int) Math.Ceiling(pageCount),
                AvailableRows = _context.Comments.Count(c => c.Author.Username == username),
                Data = await _context.Comments
                    .Where(c => c.Author.Username == username)
                    .Include(c => c.Poll)
                    .OrderByDescending(c => c.CreatedAt)
                    .Skip(skip)
                    .Take(PageSize)
                    .ToListAsync()
            };
            
            return paginatedWrapper;
        }

        public async Task<PaginatedWrapperDto<List<Comment>>> FindPaginatedByPollId(Guid pollId, int page)
        {
            var skip = (page - 1) * PageSize;
            var pageCount = (double)_context.Comments.Count() / PageSize;

            var paginatedWrapper = new PaginatedWrapperDto<List<Comment>>
            {
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(pageCount),
                AvailableRows = _context.Comments.Count(c => c.Poll.Id == pollId),
                Data = await _context.Comments
                    .Where(c => c.Poll.Id == pollId)
                    .OrderByDescending(c => c.CreatedAt)
                    .Skip(skip)
                    .Take(PageSize)
                    .ToListAsync()
            };
            Console.WriteLine(paginatedWrapper.PageCount);
            return paginatedWrapper;
        }
    }
}
