using System.ComponentModel.DataAnnotations;

namespace MediMatch.Server.Models
{
    public class Bill
    {
        [Key]
        public int Bill_Id { get; set; }
        public string Bill_details { get; set; } = null!;
        DateTime Date_received  { get; set; }
        public string cardNum { get; set; }
        public string paymentType { get; set; }
        public string UserId { get; set; }
        public bool Paid { get; set; }

    }
}
