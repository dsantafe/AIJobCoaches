namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Data;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class TrainingService(IMapper mapper,
        AIJobCoachesContext context) : ITrainingService
    {
        private readonly UnitOfWork _unitOfWork = new(context);

        public IEnumerable<TrainingDTO> GetTrainings()
        {
            IEnumerable<Training> trainings = _unitOfWork.Repository<Training>().Get().ToList();
            return mapper.Map<IEnumerable<TrainingDTO>>(trainings);
        }

        public void CreateTraining(TrainingDTO trainingDTO)
        {
            Training training = mapper.Map<Training>(trainingDTO);
            _unitOfWork.Repository<Training>().Insert(training);
            _unitOfWork.Save();
        }
    }
}
