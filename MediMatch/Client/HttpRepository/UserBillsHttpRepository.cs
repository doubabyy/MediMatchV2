using MediMatch.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediMatch.Client.HttpRepository
{
    public class UserBillsHttpRepository : IUserBillsRepository
    {
        private readonly HttpClient _httpClient;

        public UserBillsHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BillDisplay>> GetBillsHistory()
        {
            var response = await _httpClient.GetAsync("api/bill/get-bills-history");
            var content = await response.Content.ReadAsStringAsync();
            var billsHistory = JsonSerializer.Deserialize<List<BillDisplay>>(content);
            return billsHistory;
        }

        public async Task<List<BillDisplay>> GetBillsUpcoming()
        {
            var response = await _httpClient.GetAsync("api/bill/get-bills-upcoming");
            var content = await response.Content.ReadAsStringAsync();
            var billsUpcoming = JsonSerializer.Deserialize<List<BillDisplay>>(content);
            return billsUpcoming;
        }
    }
}

