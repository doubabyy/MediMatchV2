using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediMatch.Shared
{
    public class Bill
    {
        [Key]
        public int Bill_Id { get; set; }
        public string Bill_details { get; set; } = null!;
        public DateTime? Date_received  { get; set; }
        public string CardNum { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string PaymentType { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public bool Paid { get; set; }
    }
}
