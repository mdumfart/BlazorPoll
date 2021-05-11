using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Exceptions;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BlazorPoll.Server.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersDao _userDao;
        private readonly PasswordHasher<UserCredentialsDto> _hasher = new PasswordHasher<UserCredentialsDto>();


        public UsersService(IUsersDao userDao)
        {
            _userDao = userDao;
        }
        
        public async Task<User> Register(UserCredentialsDto userCredentials)
        {
            if (await _userDao.FindByUserName(userCredentials.UserName) != null)
            {
                throw new UserAlreadyExistsException(userCredentials.UserName);
            }

            var user = new User
            {
                Username = userCredentials.UserName,
                Password = _hasher.HashPassword(userCredentials, userCredentials.Password),
                CreatedAt = DateTime.Now
            };

            return await _userDao.Create(user);
        }

        public async Task<User> Login(UserCredentialsDto userCredentials, HttpContext context)
        {
            var user = await _userDao.FindByUserName(userCredentials.UserName);

            if (user != null)
            {
                if (VerifyUser(userCredentials, user) == PasswordVerificationResult.Success)
                {
                    var claim = new Claim(ClaimTypes.Name, user.Username);

                    var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync(claimsPrincipal);
                }
            }

            return await Task.FromResult(user);
        }

        public async Task<List<User>> FindAll()
        {
            return await _userDao.FindAll();
        }

        public async Task<User> FindByUserName(string userName)
        {
            return await _userDao.FindByUserName(userName);
        }

        private PasswordVerificationResult VerifyUser(UserCredentialsDto providedCredentials, User storedUser)
        {
            var storedCredentials = new UserCredentialsDto()
                { UserName = storedUser.Username, Password = storedUser.Password };
            
            return _hasher.VerifyHashedPassword(storedCredentials, storedCredentials.Password, providedCredentials.Password);
        }
    }
}
