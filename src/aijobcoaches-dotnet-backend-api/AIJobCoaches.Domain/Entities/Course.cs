namespace AIJobCoaches.Domain.Entities
{
    public class Course
    {
        public string id { get; set; }
        public int TrainingID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Instructor { get; set; }
        public string Rating { get; set; }
    }
}
