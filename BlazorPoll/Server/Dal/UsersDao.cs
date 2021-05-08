using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Server.Data;
using BlazorPoll.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorPoll.Server.Dal
{
    public class UsersDao : IUsersDao
    {
        private readonly ApplicationDbContext _context;

        public UsersDao(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(User user)
        {
            var t = _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> FindAll()
        {
            return _context.Users.ToList();
        }
    }
}
