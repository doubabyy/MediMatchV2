using MediMatch.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediMatch.Client.HttpRepository
{
    public interface IDoctorRepository
    {
        Task<List<DoctorDto>> GetDoctorsAsync();
        Task SendRequestAsync(string doc_id);
    }
}
