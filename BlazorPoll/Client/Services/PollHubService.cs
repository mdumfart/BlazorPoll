using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorPoll.Client.Services
{
    public class PollHubService : IPollHubService
    {
        private HubConnection _hubConnection;
        private readonly string url;

        public PollHubService(string url)
        {
            this.url = url;
        }

        public async Task StartPollHubConnection(Poll poll)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            _hubConnection.On<Poll>("UpdatePoll", (poll) =>
            {
                Console.WriteLine(poll.Id);
            });
            
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<Poll>("JoinPollGroup", poll);
        }

        public async Task EndPollHubConnection(Poll poll)
        {
            await _hubConnection.InvokeAsync<Poll>("LeavePollGroup", poll);
        }

        public Task Send(Poll poll) =>
            _hubConnection.SendAsync("SendPollToGroup", poll);

        public async Task Dispose(Poll poll)
        {
            await EndPollHubConnection(poll);
            await _hubConnection.DisposeAsync();
        }
    }
}
