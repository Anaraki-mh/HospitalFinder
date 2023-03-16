using HospitalFinder.Domain.Users;

namespace HospitalFinder.Core.DataAccess
{
    public interface ILockoutRepository : IRepository<Lockout>
    {
        Task<Lockout>? GetByIPAsync(string ipAddress);
    }
}