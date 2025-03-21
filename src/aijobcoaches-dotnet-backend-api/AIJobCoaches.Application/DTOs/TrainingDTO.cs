namespace AIJobCoaches.Application.DTOs
{
    public class TrainingDTO
    {
        public int TrainingID { get; set; }
        public string TrainingName { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public List<TopicDTO> Topics { get; set; }
    }
}
