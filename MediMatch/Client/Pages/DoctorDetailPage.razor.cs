using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class DoctorDetailPage
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Parameter]
        public string? Doctor_id { get; set; }
        public DoctorDto Doctor { get; set; } = new DoctorDto();


        protected override async Task OnInitializedAsync()
        {
            try
            {
                var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;

                if (UserAuth is not null && UserAuth.IsAuthenticated)
                {
                    Doctor = await Http.GetFromJsonAsync<DoctorDto>("api/doctor/get-doctor/" + Doctor_id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
