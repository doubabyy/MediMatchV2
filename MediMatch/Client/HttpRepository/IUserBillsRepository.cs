using System.Collections.Generic;
using System.Threading.Tasks;
using MediMatch.Shared;

namespace MediMatch.Client.HttpRepository
{
    public interface IUserBillsRepository
    {
        Task<List<BillDisplay>> GetBillsHistory();
        Task<List<BillDisplay>> GetBillsUpcoming();
    }
}
