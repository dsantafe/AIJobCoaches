namespace AIJobCoaches.Application.Services
{
    using AIJobCoaches.Application.DTOs;
    using AIJobCoaches.Application.Interfaces;
    using AIJobCoaches.Application.Utils;
    using AIJobCoaches.Domain.Data;
    using AIJobCoaches.Domain.Entities;
    using AutoMapper;

    public class AccountService(IMapper mapper,
        AIJobCoachesContext context) : IAccountService
    {
        private readonly UnitOfWork _unitOfWork = new(context);

        public AccountDTO Login(string username,
            string password)
        {
            string passwordHash = password.EncodeToBase64();
            var account = _unitOfWork.Repository<Account>()
                .Get(x => x.Username == username && passwordHash == x.PasswordHash, includeProperties: "Employee")
                .FirstOrDefault();
            return mapper.Map<Account, AccountDTO>(account);
        }
    }
}
