using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class AppointmentDto
    {
        public string AppointmentId { get; set; }
        public string DoctorId { get; set; }
     
        //public string PatientId { get; set; }
        //public DateTime AppointmentDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
