using AIJobCoaches.Domain.Entities;

namespace AIJobCoaches.Application.Interfaces
{
    public interface ICourseService
    {
        Task CreateCourses();
        IEnumerable<CourseDTO> GetCoursesByTraining(int trainingID);
    }
}