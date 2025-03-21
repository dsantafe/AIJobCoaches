namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        public int RoleID { get; set; }
        public Role Role { get; set; }

        public Account Account { get; set; }
        public List<Enrollment> Enrollments { get; set; }
        public List<QuizResult> QuizResults { get; set; }
    }
}
