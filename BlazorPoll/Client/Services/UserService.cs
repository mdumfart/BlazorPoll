using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Newtonsoft.Json;

namespace BlazorPoll.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> Register(UserCredentialsDto userCredentials)
        {
            var resp = await _httpClient.PostAsync("api/users/register", GetStringContent(userCredentials));

            if (resp.IsSuccessStatusCode)
            {
                var user = await resp.Content.ReadAsAsync<User>();
                return user;
            }

            return null;
        }

        public async Task<User> Login(UserCredentialsDto userCredentials)
        {
            var resp = await _httpClient.PostAsync("api/users/login", GetStringContent(userCredentials));

            if (resp.IsSuccessStatusCode)
            {
                var user = await resp.Content.ReadAsAsync<User>();
                return user;
            }

            return null;
        }

        public async Task Logout()
        {
            var resp = await _httpClient.PostAsync("api/users/logout", null);
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
