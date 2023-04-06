using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MediMatch.Shared;
using Radzen;
using Radzen.Blazor;
using Microsoft.AspNetCore.WebUtilities;


namespace MediMatch.Client.Pages
{
    public partial class ViewDoctorAppointments
    {
        [Inject]
        public HttpClient Http { get; set; }
        
        [Parameter]
        public string DoctorId { get; set; }
        public List<AppointmentDto> ExistingAppointmentsList { get; set; } = new List<AppointmentDto>();

        public AppointmentDto NewAppointment { get; set; } = new AppointmentDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        RadzenScheduler<AppointmentData> scheduler;
        Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        IList<AppointmentData> appointments = new List<AppointmentData>();
        protected override async Task OnInitializedAsync()
        {
            
            ExistingAppointmentsList = await Http.GetFromJsonAsync<List<AppointmentDto>>($"api/appointment/get-doctor-availability/{DoctorId}");
            foreach (var appointment in ExistingAppointmentsList)
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

        async Task OnSlotSelect(SchedulerSlotSelectEventArgs args)
        {
            if (args.View.Text != "Year")
            {
                AppointmentData data = await DialogService.OpenAsync<AddAppointmentPage>("Add Appointment",
                    new Dictionary<string, object> { { "Start", args.Start }, { "End", args.End } },
                    new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true }
                    );

                if (data != null)
                {
                    appointments.Add(data);
                    // Either call the Reload method or reassign the Data property of the Schedule
                    int newAppt = appointments.Count();
                    NewAppointment.DoctorId = DoctorId;
                    NewAppointment.StartTime = appointments[newAppt - 1].Start;
                    NewAppointment.EndTime = appointments[newAppt - 1].End;
                    //await scheduler.Reload();
                    
                    var response = await Http.PostAsJsonAsync("api/appointment/new-appointment", NewAppointment);
                    if (response.IsSuccessStatusCode)
                    {
                        await scheduler.Reload();
                    }
                    
                }
            }
        }
        /*
        async Task OnAppointmentSelect(SchedulerAppointmentSelectEventArgs<AppointmentData> args)
        {

            await DialogService.OpenAsync<EditAppointmentPage>("Edit Appointment", new Dictionary<string, object> { { "Appointment", args.Data } });

            await scheduler.Reload();
        }
        */
        
    }
}
