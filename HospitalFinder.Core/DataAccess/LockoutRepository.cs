using HospitalFinder.Core.Database;
using HospitalFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;


namespace HospitalFinder.Core.DataAccess
{
    public class LockoutRepository : Repository<Lockout>, ILockoutRepository
    {
        #region Constructor

        public LockoutRepository(HospitalFinderContext hospitalFinderContext) : base(hospitalFinderContext)
        {

        }

        #endregion


        #region Methods

        public async Task<Lockout>? GetByIPAsync(string ipAddress)
        {
            return await _context.Set<Lockout>().FirstOrDefaultAsync(x => x.IPAddress == ipAddress);
        }

        #endregion
    }
}
