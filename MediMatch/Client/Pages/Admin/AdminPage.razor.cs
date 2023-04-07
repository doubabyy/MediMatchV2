/*
using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages.Admin
{
    public partial class AdminPage
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<PatientDto> Patients { get; set; } = new List<PatientDto>();
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
        public List<Match> Matches { get; set; } = new List<Match>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                Patients = await Http.GetFromJsonAsync<List<PatientDto>>("api/admin/get-patients");
                Doctors = await Http.GetFromJsonAsync<List<DoctorDto>>("api/admin/get-doctors");
            }

        }

        private async Task DeletePatient(string patient_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-user", patient_id);
            Patients = await Http.GetFromJsonAsync<List<PatientDto>>("api/admin/get-patients");
            StateHasChanged();
        }

        private async Task DeleteDoctor(string doctor_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-user", doctor_id);
            Doctors = await Http.GetFromJsonAsync<List<DoctorDto>>("api/admin/get-doctors");
            StateHasChanged();
        }
       

    }
}
*/
