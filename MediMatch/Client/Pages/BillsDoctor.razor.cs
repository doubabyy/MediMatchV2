using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using MediMatch.Client.HttpRepository;

namespace MediMatch.Client.Pages
{
    public partial class BillsDoctor
    {
        //[Inject]
        //public HttpClient Http { get; set; } 

        //public IUserBillsRepository UserBillsRepository { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<BillDisplay> BillsHist { get; set; } = new List<BillDisplay>();
        public List<BillDisplay> BillsUpcoming { get; set; } = new List<BillDisplay>();
        //public HttpClient PublicHttpClient => Http;

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            bool x = (UserAuth is not null && UserAuth.IsAuthenticated);
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                BillsHist = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-history");
                BillsUpcoming = await Http.GetFromJsonAsync<List<BillDisplay>>("api/doctor/get-bills-upcoming");
            }

        }

    }
}
