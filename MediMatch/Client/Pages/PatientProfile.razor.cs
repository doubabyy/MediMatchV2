using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MediMatch.Shared;

namespace MediMatch.Client.Pages
{
    public partial class PatientProfile
    {
        [Inject]
        public HttpClient Http { get; set; }
        public PatientDto? PatientDto { get; set; } = new PatientDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {

            PatientDto = await Http.GetFromJsonAsync<PatientDto>("api/patient/patient-profile");


        }
        private async Task NewPatientInputs()
        {
            var response = await Http.PutAsJsonAsync("api/patient/patient-profile", PatientDto);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/PatientPage");
            }
        }
    }
}
