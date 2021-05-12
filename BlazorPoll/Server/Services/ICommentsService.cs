using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public interface ICommentsService
    {
        Task<Comment> Create(Comment comment);
        Task<PaginatedWrapperDto<List<Comment>>> FindByUsernamePaginated(string username, int page);
        Task<List<Comment>> FindByPollId(Guid pollId);
    }
}
