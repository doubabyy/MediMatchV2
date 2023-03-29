using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class Bills
    {
        //[Inject]
        //public HttpClient Http { get; set; } = new();
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<Bill> BillsHist { get; set; } = new List<Bill>();
        public List<Bill> BillsUpcoming { get; set; } = new List<Bill>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                BillsHist = await Http.GetFromJsonAsync<List<Bill>>("api/get-bills-history");
                BillsUpcoming = await Http.GetFromJsonAsync<List<Bill>>("api/get-bills-upcoming");
            }

        }

        private async void MakePayment(Bill b)
        {
            var res = await Http.PostAsJsonAsync("api/make-payment", b);
        }

    }
}
