using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
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

        public async Task<List<Comment>> FindByUsername(string username)
        {
            return await _commentsDao.FindByUsername(username);
        }

        public async Task<List<Comment>> FindByPollId(Guid pollId)
        {
            return await _commentsDao.FindByPollId(pollId);
        }
    }
}
