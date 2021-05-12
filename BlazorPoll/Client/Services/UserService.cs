using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BlazorPoll.Client.Services
{
    public class UserService : IUserService
    {
        private User _authenticatedUser;
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient, AuthenticationStateProvider authenticationState)
        {
            _httpClient = httpClient;

            authenticationState.AuthenticationStateChanged +=
                (Task<AuthenticationState> authenticationStateTask) => ChangeAuthenticatedUser();
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

        public async Task<User> GetAuthenticatedUser()
        {
            if (_authenticatedUser == null)
            {
                await ChangeAuthenticatedUser();
            }
            
            return _authenticatedUser;
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        
        private async Task ChangeAuthenticatedUser()
        {
            var user = await _httpClient.GetFromJsonAsync<User>("api/users/current");

            _authenticatedUser = user?.Username != null ? user : null;
        }
    }
}
