namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Data;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class QuizResultService(IMapper mapper,
        AIJobCoachesContext context) : IQuizResultService
    {
        private readonly UnitOfWork _unitOfWork = new(context);

        public void CreateQuizResult(QuizResultDTO quizResultDTO)
        {
            QuizResult quizResult = mapper.Map<QuizResult>(quizResultDTO);
            quizResult.Score = (decimal)quizResult.QuizResponses.Count(x => x.IsCorrect) / quizResult.QuizResponses.Count;
            quizResult.ResponseDate = DateTime.UtcNow;
            _unitOfWork.Repository<QuizResult>().Insert(quizResult);
            _unitOfWork.Save();
        }
    }
}
