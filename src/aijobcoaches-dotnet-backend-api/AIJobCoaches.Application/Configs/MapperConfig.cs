namespace AIJobCoaches.Application.Configs
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Training, TrainingDTO>().ReverseMap();
            CreateMap<Topic, TopicDTO>().ReverseMap();
            CreateMap<Item, ItemDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<QuizResult, QuizResultDTO>().ReverseMap();
            CreateMap<QuizResponse, QuizResponseDTO>().ReverseMap();
        }
    }
}
