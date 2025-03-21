namespace AIJobCoaches.Application.DTOs
{
    public class QuizResponseDTO
    {
        public string Question { get; set; }
        public string SelectedOption { get; set; }
        public string CorrectOption { get; set; }
        public bool IsCorrect { get; set; }
    }
}
