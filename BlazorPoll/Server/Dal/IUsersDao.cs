using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Dal
{
    public interface IUsersDao
    {
        public Task<User> Create(User user);
        public Task<User> Update(User user);
        public Task Delete(User user);
        public Task<List<User>> FindAll();
    }
}
