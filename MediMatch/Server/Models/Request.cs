namespace MediMatch.Server.Models
{
    public class Request
    {
        public int RequestID { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
        // should there be two user IDs
        public int UserId { get; set; }

    }
}
