namespace MediMatch.Server.Models
{
    public class Bill
    {
        public int Bill_num  { get; set; }
        public string Bill_details { get; set; } 
        DateTime Date_received  { get; set; }
        public string cardNum { get; set; }
        public string paymentType { get; set; }
        public int UserId { get; set; }

    }
}
