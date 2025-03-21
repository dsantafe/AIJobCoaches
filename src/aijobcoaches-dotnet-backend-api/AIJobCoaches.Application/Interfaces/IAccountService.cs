namespace AIJobCoaches.Application.Interfaces
{
    using AIJobCoaches.Application.DTOs;

    public interface IAccountService
    {
        AccountDTO Login(string username, string password);
    }
}