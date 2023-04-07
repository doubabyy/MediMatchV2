using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using MediMatch.Shared;
using System.Net.Http.Json;
using MediMatch.Client.HttpRepository;


namespace MediMatch.Client.Pages
{
    public partial class BrowseDoctors
    {
        [Inject]
        public IDoctorRepository DoctorRepository { get; set; }
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<DoctorDto> Doctors = new List<DoctorDto>();
        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                Doctors = await Http.GetFromJsonAsync<List<DoctorDto>>("api/patient/browse-doctors");
            }
        }

        private async void SendRequest(string doc_id)
        {
            await Http.PostAsJsonAsync("api/patient/send-request", doc_id);
            Doctors = await Http.GetFromJsonAsync<List<DoctorDto>>("api/patient/browse-doctors");
            StateHasChanged();
        }


    }
}
