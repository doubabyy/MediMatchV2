using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediMatch.Shared
{
    public class BillDisplay
    {
        [Key]
        public int Bill_Id { get; set; }
        public DateTime? Date_received { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public bool Paid { get; set; }
    }
}
