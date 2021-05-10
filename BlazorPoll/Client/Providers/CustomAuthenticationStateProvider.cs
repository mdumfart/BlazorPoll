using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorPoll.Client.Providers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User currentUser = await _httpClient.GetFromJsonAsync<User>("api/users/current");
            Console.WriteLine(currentUser);

            if (currentUser?.Username != null)
            {
                var claim = new Claim(ClaimTypes.Name, currentUser.Username);

                var claimsIdentity = new ClaimsIdentity(new[] { claim }, "serverAuth");

                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}
