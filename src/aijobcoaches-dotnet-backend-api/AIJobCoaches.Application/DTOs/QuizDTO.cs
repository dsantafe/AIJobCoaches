namespace AIJobCoaches.Application.DTOs
{
    using System;
    using System.Collections.Generic;

    public class QuizDTO
    {
        public Guid Id { get; set; }
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        public int TopicID { get; set; }
        public List<QuestionDTO> Questions { get; set; }
    }

    public class QuestionDTO
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public bool CorrectAnswer { get; set; }
    }
}
