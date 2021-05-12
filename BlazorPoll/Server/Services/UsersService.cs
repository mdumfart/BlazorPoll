using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Server.Dal;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Exceptions;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorPoll.Server.Services
{
    public class UsersService : IUserService
    {
        private readonly IUsersDao _userDao;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly PasswordHasher<UserCredentialsDto> _hasher = new PasswordHasher<UserCredentialsDto>();
        private readonly IConfiguration _configuration;


        public UsersService(IUsersDao userDao, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userDao = userDao;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        
        public async Task<RegisterResultDto> Register(UserCredentialsDto userCredentials)
        {
            if (await _userDao.FindByUserName(userCredentials.UserName) != null)
            {
                throw new UserAlreadyExistsException(userCredentials.UserName);
            }

            var newUser = new IdentityUser() { UserName = userCredentials.UserName};

            var result = await _userManager.CreateAsync(newUser, userCredentials.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return new RegisterResultDto() { Successful = false, Errors = errors };
            }

            return new RegisterResultDto() { Successful = true };
        }

        public async Task<LoginResultDto> Login(UserCredentialsDto userCredentials, HttpContext context)
        {
            var result =
                await _signInManager.PasswordSignInAsync(
                    userCredentials.UserName, userCredentials.Password, false, false);

            if (!result.Succeeded)
            {
                return new LoginResultDto {Successful = false, Error = "Username and password are invalid."};
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userCredentials.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: credentials
            );

            return new LoginResultDto { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) };
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
