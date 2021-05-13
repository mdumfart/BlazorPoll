using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlazorPoll.Server.Services
{
    public interface IUserService
    {
        public Task<RegisterResultDto> Register(UserCredentialsDto userCredentials);
        public Task<LoginResultDto> Login(UserCredentialsDto userCredentials, HttpContext context);
        public Task<IdentityUser> FindByUserName(string userName);
    }
}
