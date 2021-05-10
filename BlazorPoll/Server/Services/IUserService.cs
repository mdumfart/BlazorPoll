using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace BlazorPoll.Server.Services
{
    public interface IUserService
    {
        public Task<User> Register(UserCredentialsDto userCredentials);
        public Task<User> Login(UserCredentialsDto userCredentials, HttpContext context);
        public Task<List<User>> FindAll();
    }
}
