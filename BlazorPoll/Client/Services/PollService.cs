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

        public async Task<Poll> FindPollById(Guid id)
        { 
            return await httpClient.GetFromJsonAsync<Poll>($"/api/polls/{id}");
        }

        public async Task<bool> SendSinglePollAnswer(Poll poll, Answer answer, IPollHubService pollHubService)
        {
            var resp = await httpClient.PutAsync($"/api/polls/{poll.Id}/vote-single/{answer.Id}", GetStringContent(answer));

            if (resp.IsSuccessStatusCode)
            {
                var updatedPoll = await FindPollById(poll.Id);
                await pollHubService.Send(updatedPoll);
            }
            
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> SendMultiplePollAnswers(Poll poll, List<Answer> answers, IPollHubService pollHubService)
        {
            var ids = GetIdArrayFromAnswerList(answers);
            
            var resp = await httpClient.PutAsync($"/api/polls/{poll.Id}/vote-multiple", GetStringContent(ids));

            if (resp.IsSuccessStatusCode)
            {
                var updatedPoll = await FindPollById(poll.Id);
                await pollHubService.Send(updatedPoll);
            }

            return resp.IsSuccessStatusCode;
        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);
            
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private int[] GetIdArrayFromAnswerList(List<Answer> answers)
        {
            var ids = new int[answers.Count];

            for (int i = 0; i < answers.Count; i++)
            {
                ids[i] = answers[i].Id;
            }

            return ids;
        }
    }
}
