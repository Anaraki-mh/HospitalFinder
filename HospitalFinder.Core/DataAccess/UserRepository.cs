using HospitalFinder.Core.Database;
using HospitalFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace HospitalFinder.Core.DataAccess
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        #region Constructor

        public UserRepository(HospitalFinderContext hospitalFinderContext) : base(hospitalFinderContext)
        {

        }

        #endregion


        #region Methods

        public async Task<User>? GetByEmailAsync(string emailAddress)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.EmailAddress == emailAddress);
        }

        public async Task<User>? GetByTokanHashAsync(string tokenHash)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
        }

        #endregion
    }
}
