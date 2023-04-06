using System.ComponentModel.DataAnnotations.Schema;

namespace MediMatch.Shared
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set;}
        [ForeignKey("Doctors")]
        public string DoctorId { get; set; }
        [ForeignKey("Patients")]
        public string PatientId { get; set; }
        public DateTime AppointmentDateStart { get; set; }
        public DateTime AppointmentDateEnd { get; set; }
     
        
        

    }
}
