namespace AIJobCoaches.Application.DTOs
{
    public class EmployeeDTO
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public RoleDTO Role { get; set; }
    }
}
