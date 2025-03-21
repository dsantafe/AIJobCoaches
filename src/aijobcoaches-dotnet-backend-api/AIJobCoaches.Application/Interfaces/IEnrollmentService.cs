namespace AIJobCoaches.Application.Interfaces
{
    using AIJobCoaches.Application.DTOs;

    public interface IEnrollmentService
    {
        IEnumerable<TrainingDTO> GetTrainingsByEmployeeID(int employeeID);
    }
}