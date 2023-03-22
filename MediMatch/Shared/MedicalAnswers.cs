namespace MediMatch.Server.Models
{
    public class MedicalAnswers
    {
        public int Med_ans_ID { get; set; }
        public int question_ID { get; set; }
        public string answer { get; set; }
        public int UserId { get; set; }

    }
}
