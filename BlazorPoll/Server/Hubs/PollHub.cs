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
        public async Task SendPollToGroup(Poll poll)
        {
            await Clients.Groups(poll.Id.ToString()).SendAsync("UpdatePoll", poll);
        }

        public async Task JoinPollGroup(Poll poll)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, poll.Id.ToString());
        }

        public async Task LeavePollGroup(Poll poll)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, poll.Id.ToString());
        }
    }
}
