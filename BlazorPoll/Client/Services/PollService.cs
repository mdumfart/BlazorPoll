using System;
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
        
        public async Task<bool> AddPoll(Poll poll)
        {
            var resp = await httpClient.PostAsync("api/polls", GetStringContent(poll));

            if (resp.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<Poll> GetPollById(int id)
        {
            return await httpClient.GetFromJsonAsync<Poll>("/api/polls/{id}");
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);
            
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
