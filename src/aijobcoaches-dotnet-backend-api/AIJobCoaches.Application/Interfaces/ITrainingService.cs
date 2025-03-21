namespace AIJobCoaches.Application.Interfaces
{
    using AIJobCoaches.Application.DTOs;

    public interface ITrainingService
    {
        IEnumerable<TrainingDTO> GetTrainings();
        void CreateTraining(TrainingDTO trainingDTO);
    }
}