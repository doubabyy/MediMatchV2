using MediMatch.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
namespace MediMatch.Client.HttpRepository
{
    public class DoctorHttpRepository
    {
        private readonly HttpClient _httpClient;

        public DoctorHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DoctorDto>> GetDoctorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DoctorDto>>("api/patient/browse-doctors");
        }

        public async Task SendRequestAsync(string doc_id)
        {
            await _httpClient.PostAsJsonAsync("api/patient/send-request", doc_id);
        }
    }
}
