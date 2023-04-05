﻿using MediMatch.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class ViewRequests
    {
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<MatchDto> Requests { get; set; } = new List<MatchDto>();

        protected override async Task OnInitializedAsync()
        {
            Requests = await Http.GetFromJsonAsync<List<MatchDto>>("api/get-requests");
        }

        private async Task AcceptRequest(int request_id)
        {
            await Http.PostAsJsonAsync("api/accept-request", request_id);
            Requests = await Http.GetFromJsonAsync<List<MatchDto>>("api/get-requests");
            StateHasChanged();
        }

        // Add a reject Method 
        private async Task RejectRequest(int request_id)
        {
            await Http.PostAsJsonAsync("api/reject-request", request_id);
            Requests = await Http.GetFromJsonAsync<List<MatchDto>>("api/get-requests");
            StateHasChanged();
        }
    }
}
