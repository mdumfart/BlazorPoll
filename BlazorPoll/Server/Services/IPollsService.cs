using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public interface IPollsService
    {
        public Task<Poll> Create(Poll poll);
        public Task<Poll> Update(Poll poll);
        public Task Delete(Poll poll);
        public Task<Poll> FindById(Guid id);
        public Task<List<Poll>> FindAll();
        public Task<List<Poll>> FindByAuthorName(string authorName);
    }
}
