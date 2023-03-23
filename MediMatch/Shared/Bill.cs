namespace MediMatch.Shared
{
    public class Bill
    {
        public int Bill_Id { get; set; }
        public string Bill_details { get; set; } = null!;
        DateTime Date_received  { get; set; }
        public string cardNum { get; set; } = null!;
        public string paymentType { get; set; } = null!;
        public int UserId { get; set; }

    }
}
