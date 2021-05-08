using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public interface IUserService
    {
        public Task<User> Register(User user);
        //public Task<User> Login(User user);
        public Task<List<User>> FindAll();
    }
}
