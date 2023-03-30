namespace MediMatch.Shared
{
    public class Request
    {
        public int RequestID { get; set; }
        public string RequestType { get; set; } = null!;
        public string RequestStatus { get; set; } = null!;
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
    }
}