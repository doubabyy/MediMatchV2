using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MediMatch.Shared;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.WebUtilities;


namespace MediMatch.Client.Pages
{
    public partial class ViewAppointments
    {
        [Inject]
        public HttpClient Http { get; set; }
        public List<AppointmentDto> AppointmentsList { get; set; } = new List<AppointmentDto>();
        public NavigationManager NavigationManager { get; set; }

        RadzenScheduler<AppointmentData> scheduler;
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        IList<AppointmentData> appointments = new List<AppointmentData>();

        protected override async Task OnInitializedAsync()
        {

            AppointmentsList = await Http.GetFromJsonAsync<List<AppointmentDto>>($"api/appointment/get-appointments");
            foreach (var appointment in AppointmentsList)
            {
                appointments.Add(new AppointmentData { Start = appointment.StartTime, End = appointment.EndTime, Text = "Booked" });
            }

            await scheduler.Reload();
        }

        void OnSlotRender(SchedulerSlotRenderEventArgs args)
        {
            // Highlight today in month view
            if (args.View.Text == "Month" && args.Start.Date == DateTime.Today)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }

            // Highlight working hours (9-18)
            if ((args.View.Text == "Week" || args.View.Text == "Day") && args.Start.Hour > 8 && args.Start.Hour < 19)
            {
                args.Attributes["style"] = "background: rgba(255,220,40,.2);";
            }
        }



    }
}
