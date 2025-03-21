namespace AIJobCoaches.Application.DTOs
{
    public class QuizResultDTO
    {
        public string QuizID { get; set; }
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        public int TopicID { get; set; }
        public List<QuizResponseDTO> QuizResponses { get; set; }
    }
}
