using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsDao _commentsDao;

        public CommentsService(ICommentsDao commentsDao)
        {
            _commentsDao = commentsDao;
        }
        
        public async Task<Comment> Create(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            return await _commentsDao.Create(comment);
        }

        public async Task<PaginatedWrapperDto<List<Comment>>> FindByUsernamePaginated(string username, int page)
        {
            return await _commentsDao.FindByUsernamePaginated(username, page);
        }

        public async Task<PaginatedWrapperDto<List<Comment>>> FindPaginatedByPollId(Guid pollId, int page)
        {
            return await _commentsDao.FindPaginatedByPollId(pollId, page);
        }
    }
}
