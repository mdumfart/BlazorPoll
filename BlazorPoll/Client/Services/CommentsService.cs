using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Newtonsoft.Json;

namespace BlazorPoll.Client.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly HttpClient _httpClient;

        public CommentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<Comment> Create(Comment comment)
        {
            var resp = await _httpClient.PostAsync("/api/comments", GetStringContent(comment));

            if (resp.IsSuccessStatusCode)
            {
                var createdComment = await resp.Content.ReadAsAsync<Comment>();
                return createdComment;
            }

            return null;
        }

        public async Task<List<Comment>> GetByPollId(Guid pollId)
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>($"/api/polls/{pollId}/comments");

        }

        public async Task<List<Comment>> GetByUsername(string username)
        {
            return await _httpClient.GetFromJsonAsync<List<Comment>>($"/api/users/{username}/comments");

        }

        private StringContent GetStringContent(object o)
        {
            var json = JsonConvert.SerializeObject(o);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
