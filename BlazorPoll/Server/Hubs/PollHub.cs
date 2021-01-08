using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace BlazorPoll.Server.Hubs
{
    public class PollHub : Hub
    {
        public async Task UpdatePoll(Poll poll)
        {
            await Clients.All.SendAsync("UpdatePoll", poll);
        }
    }
}
