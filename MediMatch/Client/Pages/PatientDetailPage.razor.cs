using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class PatientDetailPage
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Parameter]
        public string? Patient_id { get; set; }
        public PatientDto Patient { get; set; } = new PatientDto();


        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

                if (UserAuth is not null && UserAuth.IsAuthenticated)
                {
                    Patient = await Http.GetFromJsonAsync<PatientDto>("api/patient/get-patient/" + Patient_id);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}
