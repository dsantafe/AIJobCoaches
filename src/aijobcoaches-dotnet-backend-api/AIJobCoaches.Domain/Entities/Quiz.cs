namespace AIJobCoaches.Application.Services
{
    using System.Collections.Generic;

    public class Quiz
    {
        public string id { get; set; }
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        public int TopicID { get; set; }
        public List<QuestionItem> Questions { get; set; }
    }

    public class QuestionItem
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
