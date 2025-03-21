namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Training
    {
        [Key]
        public int TrainingID { get; set; }

        [Required, MaxLength(100)]
        public string TrainingName { get; set; }

        public string Description { get; set; }

        public string Attachment { get; set; }

        public List<Topic> Topics { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<QuizResult> QuizResults { get; set; }
    }
}
