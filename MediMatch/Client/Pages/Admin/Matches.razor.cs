using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages.Admin
{
    public partial class Matches
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<MatchDto> PendingRequests { get; set; } = new List<MatchDto>();
        public List<MatchDto> AcceptedRequests { get; set; } = new List<MatchDto>();
        public List<MatchDto> RejectedRequests { get; set; } = new List<MatchDto>();

        protected override async Task OnInitializedAsync()
        {
            AcceptedRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/get-accepted-requests");
            RejectedRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/get-rejected-requests");
            PendingRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/gpr");
        }

        private async Task DeleteMatch(int match_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-match", match_id);
            PendingRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/gpr");
            AcceptedRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/get-accepted-requests");
            RejectedRequests = await Http.GetFromJsonAsync<List<MatchDto>>("api/admin/get-rejected-requests");
            StateHasChanged();
        }
    }
}
