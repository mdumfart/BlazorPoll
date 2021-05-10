using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface IUserService
    {
        Task<User> Register(UserCredentialsDto userCredentials);
        Task<User> Login(UserCredentialsDto userCredentials);
        Task Logout();
    }
}
