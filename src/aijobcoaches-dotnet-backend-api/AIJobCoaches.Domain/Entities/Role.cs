namespace AIJobCoaches.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
