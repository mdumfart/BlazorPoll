using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorPoll.Client.Services;
using BlazorPoll.Shared.Dtos;
using BlazorPoll.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorPoll.Client.Pages.Detail
{
    public partial class PollDetailsPage
    {
        [Parameter]
        public string Id { get; set; }
        public Poll Poll { get; set; }
        public bool Answered { get; set; } = false;
        public bool Copied { get; set; } = false;
        public PaginatedWrapperDto<List<Comment>> PaginatedComments { get; set; }

        private VisualizePoll VisualizePollChild;
        private bool IsLoading { get; set; } = false;
        private IPollHubService _pollHubService;
        private Answer SelectedSingleAnswer { get; set; }
        private List<Answer> SelectedMultipleAnswers { get; set; } = new List<Answer>();
        private const int PageSize = 10;


        protected override async Task OnInitializedAsync()
        {
            Guid guidFromParams = Guid.Empty;

            try
            {
                guidFromParams = new Guid(Id);
                Poll = await GetPollByIdAsync(guidFromParams);
            }
            catch (Exception e)
            {
                // When Guid creation or http call fails
                Poll = null;
            }

            if (Poll != null)
            {
                _pollHubService = new PollHubService(NavigationManager.ToAbsoluteUri("/pollhub").ToString());
                await _pollHubService.StartPollHubConnection(Poll);

                _pollHubService.PollChanged += async (poll) => await VisualizePollChild.UpdatePoll(poll);
            }

            var pageCount = (double)Poll.Comments.Count() / PageSize;
            var lastPage = pageCount == 0 ? 1 : (int)Math.Ceiling(pageCount);
            await LoadPaginatedComments(lastPage);
        }


        private async Task<Poll> GetPollByIdAsync(Guid id)
        {
            return await PollService.FindPollById(id);
        }

        public async ValueTask DisposeAsync()
        {
            await _pollHubService.Dispose(Poll);
        }

        /// <summary>
        /// Set selected item for single choice polls
        /// </summary>
        /// <param name="answer">The answer to set to currently selected</param>
        public void OnSingleSelectionChanged(Answer answer)
        {
            SelectedSingleAnswer = answer;
        }

        public void OnMultipleSelectionChange(Answer answer)
        {
            if (SelectedMultipleAnswers.Contains(answer))
            {
                SelectedMultipleAnswers.Remove(answer);
            }
            else
            {
                SelectedMultipleAnswers.Add(answer);
            }
        }

        private async Task SubmitAnswer()
        {
            IsLoading = true;
            bool result = false;

            if (Poll.IsMultipleChoice)
            {
                result = await PollService.SendMultiplePollAnswers(Poll, SelectedMultipleAnswers, _pollHubService);
            }
            else
            {
                result = await PollService.SendSinglePollAnswer(Poll, SelectedSingleAnswer, _pollHubService);
            }

            IsLoading = false;

            if (result)
                Answered = true;
        }

        private async Task CopyToClipBoard()
        {
            Copied = true;
            await JS.InvokeVoidAsync("copyStringToClipboard", NavigationManager.Uri);
            await Task.Delay(5000);
            Copied = false;
        }

        private async Task LoadPaginatedComments(int page)
        {
            PaginatedComments = await CommentsService.GetByPollIdPaginated(Poll.Id, page);
        }

    }
}
