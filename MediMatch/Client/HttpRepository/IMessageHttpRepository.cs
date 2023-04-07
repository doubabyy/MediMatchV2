using MediMatch.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediMatch.Client.HttpRepository
{
    public interface IMessageHttpRepository
    {
        Task<List<Message>> GetMessagesBetweenUsersAsync(string userId1, string userId2);
        Task SendMessageAsync(Message message);
    }
}
