using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public interface ICommentsService
    {
        Task<Comment> Create(Comment comment);
        Task<List<Comment>> FindByUsername(string username);
        Task<List<Comment>> FindByPollId(Guid pollId);
    }
}
