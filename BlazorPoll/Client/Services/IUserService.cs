using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace BlazorPoll.Client.Services
{
    public interface IUserService
    {
        Task<RegisterResultDto> Register(UserCredentialsDto userCredentials);
        Task<LoginResultDto> Login(UserCredentialsDto userCredentials);
        Task Logout();
        Task<IdentityUser> GetCurrentUser();
    }
}
