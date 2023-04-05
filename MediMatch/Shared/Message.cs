using System.ComponentModel.DataAnnotations.Schema;


namespace MediMatch.Shared
{
    public class Message
    { 
        public int MessageId { get; set; }
        public string MessageTxt { get; set; } = null!;
        public DateTime MessageDate { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string MessageFromID { get; set; } = null!;
        
       [ForeignKey("ApplicationUser")]
        public string MessageToID { get; set; } = null!;
    }
}