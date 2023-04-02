using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class Match
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public bool Accepted { get; set; }
    }
}
