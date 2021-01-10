using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;

namespace BlazorPoll.Client.Services
{
    public interface IPollHubService
    {
        public delegate void PollChangedHandler(Poll poll);
        public event PollChangedHandler PollChanged;
        
        Task StartPollHubConnection(Poll poll);
        Task Send(Poll poll);
        Task Dispose(Poll poll);
    }
}
