using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MediMatch.Shared;
using Radzen;
using Radzen.Blazor;


namespace MediMatch.Client.Pages
{

    public partial class ScheduleAppointments
    {
        [Inject]
        public HttpClient Http { get; set; }
        //public DoctorDto? Doctor { get; set; } = new DoctorDto();
        public string DoctorId { get; set; } = string.Empty;
        public List<DoctorDto> Doctors { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        //public SelectDoctor Docs { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            Doctors = await Http.GetFromJsonAsync<List<DoctorDto>>("api/appointment/get-doctors");

        }

        private async void ReqDoctorId()
        {
            try
            {
                NavigationManager.NavigateTo($"/ViewDoctorAppointments/{DoctorId}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}

/*public class SelectDoctor
{
    public string DoctorId { get; set; }

}
*/

