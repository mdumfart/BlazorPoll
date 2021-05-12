using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface ICommentsService
    {
        Task<Comment> Create(Comment comment);
        Task<PaginatedWrapperDto<List<Comment>>> GetByPollIdPaginated(Guid pollId, int page);
        Task<PaginatedWrapperDto<List<Comment>>> GetByUsernamePaginated(string username, int page);
    }
}
