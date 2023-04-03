using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class BillsDoctor
    {
        //[Inject]
        //public HttpClient Http { get; set; } = new();
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<BillDisplay> BillsHist { get; set; } = new List<BillDisplay>();
        public List<BillDisplay> BillsUpcoming { get; set; } = new List<BillDisplay>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                BillsHist = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-history");
                BillsUpcoming = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-upcoming");
            }

        }

        private async void MakePayment(int bill_id)
        {
            await Http.PostAsJsonAsync("api/bill/make-payment", bill_id);
            //await InvokeAsync(() => StateHasChanged());
            //StateHasChanged();
            BillsHist = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-history");
            BillsUpcoming = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-upcoming");
            StateHasChanged();
        }

    }
}
