using MediMatch.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace MediMatch.Client.Pages
{
    public partial class ViewUpcomingAppointments
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        public List<AppointmentDto> UpcomingAppointments { get; set; } = new List<AppointmentDto>();

        protected override async Task OnInitializedAsync()
        {
            var UserAuth = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
            if (UserAuth is not null && UserAuth.IsAuthenticated)
            {
                UpcomingAppointments = await Http.GetFromJsonAsync<List<AppointmentDto>>("api/appointment/view-patient-appointments");
            }
        }
    }
}
