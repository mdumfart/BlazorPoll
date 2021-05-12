using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public class PollsService : IPollsService
    {
        private readonly IPollsDao _pollsDao;

        public PollsService(IPollsDao pollsDao)
        {
            _pollsDao = pollsDao;
        }

        public async Task<Poll> Create(Poll poll)
        {
            poll.CreatedAt = DateTime.Now;
            return await _pollsDao.Create(poll);
        }

        public async Task<Poll> Update(Poll poll)
        {
            return await _pollsDao.Update(poll);
        }

        public async Task Delete(Poll poll)
        {
            await _pollsDao.Delete(poll);
        }

        public async Task<Poll> FindById(Guid id)
        {
            return await _pollsDao.FindById(id);
        }

        public async Task<List<Poll>> FindAll()
        {
            return await _pollsDao.FindAll();
        }

        public async Task<List<Poll>> FindByAuthorName(string authorName)
        {
            return await _pollsDao.FindByAuthorName(authorName);
        }
    }
}
