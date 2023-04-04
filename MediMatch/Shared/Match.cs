using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class Match
    {
        public int Id { get; set; }
        [ForeignKey("Patients")]
        public string PatientId { get; set; }
        [ForeignKey("Doctors")]
        public string DoctorId { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public bool Accepted { get; set; }
    }
}