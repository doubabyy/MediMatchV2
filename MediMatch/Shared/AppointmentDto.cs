using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class AppointmentDto
    {

        public int? AppointmentId { get; set; } = null!;
        public string DoctorId { get; set; }
        public string? DoctorFirstName { get; set; } = null!;
        public string? DoctorLastName { get; set; } = null!;

        //public string PatientId { get; set; }
        //public DateTime AppointmentDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
