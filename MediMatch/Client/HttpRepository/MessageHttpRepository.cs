using MediMatch.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace MediMatch.Client.HttpRepository
{
    public class MessageHttpRepository : IMessageHttpRepository
    {
        private readonly HttpClient _httpClient;

        public MessageHttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Message>> GetMessagesBetweenUsersAsync(string userId1, string userId2)
        {
            return await _httpClient.GetFromJsonAsync<List<Message>>($"GetMessagesBetweenUsers/{userId1}/{userId2}");
        }

        public async Task SendMessageAsync(Message message)
        {
            await _httpClient.PostAsJsonAsync("api/SendMessage", message);
        }

    }
}
