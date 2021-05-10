using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Client.Providers;
using BlazorPoll.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorPoll.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddSingleton<IPollService, PollService>(_ => 
                new PollService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }));
            
            builder.Services.AddSingleton<IUserService, UserService>(_ =>
                new UserService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }));
            
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            
            await builder.Build().RunAsync();
        }
    }
}
