using AIJobCoaches.Application.DTOs;

namespace AIJobCoaches.Application.Interfaces
{
    public interface ITopicService
    {
        IEnumerable<TopicDTO> GetTopicsWithItemsByTrainingId(int trainingID);
    }
}