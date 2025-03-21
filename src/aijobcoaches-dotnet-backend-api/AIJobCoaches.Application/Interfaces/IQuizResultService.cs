namespace AIJobCoaches.Application.Interfaces
{
    using AIJobCoaches.Application.DTOs;

    public interface IQuizResultService
    {
        void CreateQuizResult(QuizResultDTO quizResultDTO);
    }
}