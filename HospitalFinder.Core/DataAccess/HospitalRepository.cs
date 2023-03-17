using HospitalFinder.Core.Database;
using HospitalFinder.Domain.HospitalData;
using HospitalFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;


namespace HospitalFinder.Core.DataAccess
{
    public class HospitalRepository : Repository<Hospital>, IHospitalRepository
    {
        #region Constructor

        public HospitalRepository(HospitalFinderContext hospitalFinderContext) : base(hospitalFinderContext)
        {

        }

        #endregion


        #region Methods

        public async Task<List<Hospital>> GetHospitalsInRange(double xMin, double xMax, double yMin, double yMax)
        {
            //return await _context.Set<Hospital>().Where(c => c.Longtitude <= xMin && c.Longtitude >= xMax && c.Latitude <= yMin && c.Latitude >= yMax).ToListAsync();
            return await _context.Set<Hospital>().FromSqlInterpolated<Hospital>($"SELECT * FROM Hospitals WHERE (Longtitude BETWEEN {xMin} AND {xMax}) AND (Latitude BETWEEN {yMin} AND {yMax})").ToListAsync();
        }

        #endregion
    }
}
