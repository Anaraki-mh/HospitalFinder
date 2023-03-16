using HospitalFinder.Domain.Users;

namespace HospitalFinder.Core.DataAccess
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User>? GetByEmailAsync(string emailAddress);
        Task<User>? GetByTokanHashAsync(string tokenHash);
    }
}