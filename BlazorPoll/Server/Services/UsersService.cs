using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Server.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersDao _userDao;

        public UsersService(IUsersDao userDao)
        {
            _userDao = userDao;
        }
        
        public async Task<User> Register(User user)
        {
            return await _userDao.Create(user);
        }

        public async Task<List<User>> FindAll()
        {
            return await _userDao.FindAll();
        }
    }
}
