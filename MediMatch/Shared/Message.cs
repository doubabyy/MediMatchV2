namespace MediMatch.Server.Models
{
    public class Message
    { 
        public int MessageId { get; set; }
        public string MessageTxt { get; set; }
        public DateTime MessageDate { get; set; }

        public string MessageFromID { get; set; }
        public string MessageToID { get; set; }
    }
}
