using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages.Admin
{
    public partial class Patients
    {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<PatientDto> PatientsList { get; set; } = new List<PatientDto>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                PatientsList = await Http.GetFromJsonAsync<List<PatientDto>>("api/admin/get-patients");
            }

        }

        private async Task DeletePatient(string patient_id)
        {
            await Http.PostAsJsonAsync("api/admin/delete-user", patient_id);
            PatientsList = await Http.GetFromJsonAsync<List<PatientDto>>("api/admin/get-patients");
            StateHasChanged();
        }
    }
}
