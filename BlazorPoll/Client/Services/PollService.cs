﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Shared.Models;
using Newtonsoft.Json;

namespace BlazorPoll.Client.Services
{
    public class PollService : IPollService
    {
        private const string ApiUrl = "http://localhost:64418";
        private readonly HttpClient httpClient;
        
        public PollService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        
        public async Task<Guid> AddPoll(Poll poll)
        {
            var resp = await httpClient.PostAsync("api/polls", GetStringContent(poll));

            if (resp.IsSuccessStatusCode)
            {
                var resultPoll = await resp.Content.ReadAsAsync<Poll>();
                return resultPoll.Id;
            }

            return Guid.Empty;
        }

        public async Task<Poll> GetPollById(Guid id)
        {
            return await httpClient.GetFromJsonAsync<Poll>($"/api/polls/{id.ToString()}");
        }

        public async Task<bool> SendSinglePollAnswer(Poll poll, Answer answer, IPollHubService pollHubService)
        {
            var resp = await httpClient.PutAsync($"/api/polls/{poll.Id}/answer/single", GetStringContent(answer));

            if (resp.IsSuccessStatusCode)
            {
                var updatedPoll = await GetPollById(poll.Id);
                await pollHubService.Send(updatedPoll);
            }
            
            return resp.IsSuccessStatusCode;
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);
            
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
