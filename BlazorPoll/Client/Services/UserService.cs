using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using BlazorPoll.Client.Providers;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace BlazorPoll.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider,
            UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
        }

        public async Task<RegisterResultDto> Register(UserCredentialsDto userCredentials)
        {
            var resp = await _httpClient.PostAsync("api/users/register", GetStringContent(userCredentials));

            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsAsync<RegisterResultDto>();
                return result;
            }

            return null;
        }

        public async Task<LoginResultDto> Login(UserCredentialsDto userCredentials)
        {
            var resp = await _httpClient.PostAsync("api/users/login", GetStringContent(userCredentials));

            var result =  await resp.Content.ReadAsAsync<LoginResultDto>();

            if (!result.Successful)
            {
                return result;
            }

            await _localStorage.SetItemAsync("authToken", result.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userCredentials.UserName);

            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<IdentityUser> GetCurrentUser()
        {
            var userClaimsPrincipal = (await _authenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (userClaimsPrincipal.Identity != null)
            {
                return await _userManager.GetUserAsync(userClaimsPrincipal);

            }

            return null;
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
