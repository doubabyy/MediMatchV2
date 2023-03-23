namespace MediMatch.Shared
{
    public class Request
    {
        public int RequestID { get; set; }
        public string RequestType { get; set; } = null!;
        public string RequestStatus { get; set; } = null!;
        // should there be two user IDs
        public int UserId { get; set; }

    }
}
