using System.ComponentModel.DataAnnotations.Schema;


namespace MediMatch.Shared
{
    public class Message
    { 
        public int MessageId { get; set; }
        public string MessageTxt { get; set; } = null!;
        public DateTime MessageDate { get; set; }
        [ForeignKey("Users")]
        public string MessageFromID { get; set; } = null!;
        public string MessageToID { get; set; } = null!;
    }
}