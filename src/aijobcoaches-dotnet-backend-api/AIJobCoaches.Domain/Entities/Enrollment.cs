namespace AIJobCoaches.Domain.Entities
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int EmployeeID { get; set; }
        public int TrainingID { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Enrolled"; // Valores posibles: Enrolled, Completed, Dropped

        public Employee Employee { get; set; }
        public Training Training { get; set; }
    }
}
