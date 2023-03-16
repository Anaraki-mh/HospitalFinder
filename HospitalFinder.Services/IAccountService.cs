using HospitalFinder.Domain.Users;

namespace HospitalFinder.Services
{
    public interface IAccountService
    {
        Task<string> GenerateToken(User user);
        Task<bool> IsIPLockedOut(string ipAddress);
        Task<bool> IsTokenValid(string token);
        Task Lockout(string ipAddress);
        Task<(User, bool)> Login(string email, string password);
        Task<bool> SignUp(string email, string password);
    }
}