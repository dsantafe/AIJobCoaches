namespace AIJobCoaches.Application.DTOs
{
    public class TopicDTO
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}
