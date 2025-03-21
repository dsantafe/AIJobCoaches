namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Domain.Data;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class TopicService(IMapper mapper,
        AIJobCoachesContext context) : ITopicService
    {
        private readonly UnitOfWork _unitOfWork = new(context);

        public IEnumerable<TopicDTO> GetTopicsWithItemsByTrainingId(int trainingID)
        {
            var topicsWithItems = _unitOfWork.Repository<Topic>()
                .Get(x => x.TrainingID == trainingID, includeProperties: "Items")
                .ToList();
            return mapper.Map<IEnumerable<TopicDTO>>(topicsWithItems);
        }
    }
}
