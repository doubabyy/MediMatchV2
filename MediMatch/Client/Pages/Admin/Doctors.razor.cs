using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages.Admin
{
    public partial class Doctors
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<DoctorDto> DoctorsList { get; set; } = new List<DoctorDto>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                DoctorsList = await Http.GetFromJsonAsync<List<DoctorDto>>("api/admin/get-doctors");
            }

        }

        private async Task DeleteDoctor(string doctor_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-user", doctor_id);
            DoctorsList = await Http.GetFromJsonAsync<List<DoctorDto>>("api/admin/get-doctors");
            StateHasChanged();
        }


    }
}

