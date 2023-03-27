namespace MediMatch.Shared
{
    public class MedicalAnswers
    {
        public int Med_ans_ID { get; set; }
        public int question_ID { get; set; }
        public string answer { get; set; } = null!;
        public int UserId { get; set; }

    }
}
