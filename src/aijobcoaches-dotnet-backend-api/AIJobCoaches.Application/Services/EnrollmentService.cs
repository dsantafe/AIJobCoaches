namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Data;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class EnrollmentService(IMapper mapper,
        AIJobCoachesContext context) : IEnrollmentService
    {
        private readonly UnitOfWork _unitOfWork = new(context);

        public IEnumerable<TrainingDTO> GetTrainingsByEmployeeID(int employeeID)
        {
            IEnumerable<Training> trainings = _unitOfWork.Repository<Enrollment>()
                .Get(x => x.EmployeeID == employeeID, includeProperties: "Training")
                .Select(e => e.Training); // Extrae solo la capacitación
            return mapper.Map<List<TrainingDTO>>(trainings);
        }
    }
}
