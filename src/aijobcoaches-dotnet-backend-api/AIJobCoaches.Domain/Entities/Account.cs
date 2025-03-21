namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
