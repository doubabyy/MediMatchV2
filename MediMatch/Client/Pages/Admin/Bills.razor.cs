using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages.Admin
{
    public partial class Bills
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<BillDisplay> BillsUpcoming { get; set; } = new List<BillDisplay>();
        public List<BillDisplay> BillsHist { get; set; } = new List<BillDisplay>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                BillsUpcoming = await Http.GetFromJsonAsync<List<BillDisplay>>("api/admin/get-bills-upcoming");
                BillsHist = await Http.GetFromJsonAsync<List<BillDisplay>>("api/admin/get-bills-history");
            }
        }

        private async Task DeleteBill(int bill_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-bill", bill_id);
            BillsUpcoming = await Http.GetFromJsonAsync<List<BillDisplay>>("api/admin/get-bills-upcoming");
            BillsHist = await Http.GetFromJsonAsync<List<BillDisplay>>("api/admin/get-bills-history");
            StateHasChanged();
        }


    }
}

