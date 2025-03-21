namespace AIJobCoaches.Application.DTOs
{
    public class AccountDTO
    {
        public int AccountID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int EmployeeID { get; set; }
        public EmployeeDTO? Employee { get; set; }
    }
}
