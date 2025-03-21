namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class QuizResult
    {
        [Key]
        public int QuizResultID { get; set; }

        public string QuizID { get; set; }

        public decimal Score { get; set; }

        public DateTime ResponseDate { get; set; }

        public int EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }

        public int TrainingID { get; set; }
        public virtual Training Training { get; set; }

        public int TopicID { get; set; }
        public virtual Topic Topic { get; set; }

        public virtual ICollection<QuizResponse> QuizResponses { get; set; }
    }
}
