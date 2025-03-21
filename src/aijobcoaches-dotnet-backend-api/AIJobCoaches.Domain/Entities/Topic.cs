namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        [Key]
        public int TopicID { get; set; }

        public int TrainingID { get; set; }
        public Training Training { get; set; }

        [Required, MaxLength(100)]
        public string TopicName { get; set; }

        public List<Item> Items { get; set; }
        public List<QuizResult> QuizResults { get; set; }
    }
}
