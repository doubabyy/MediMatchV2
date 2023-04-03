﻿using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using MediMatch.Shared;


namespace MediMatch.Client.Pages

{
    public partial class DoctorProfile
    {
        [Inject]
        public HttpClient Http { get; set; }
        public DoctorDto? DoctorDto { get; set; } = new DoctorDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
           
            DoctorDto = await Http.GetFromJsonAsync<DoctorDto>("api/doctor/doctor-profile");
            
          
        }
        private async Task NewDoctorInputs()
        {
            var response = await Http.PutAsJsonAsync("api/doctor/doctor-profile", DoctorDto);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/DoctorPage");
            }
        }
    }
}
 