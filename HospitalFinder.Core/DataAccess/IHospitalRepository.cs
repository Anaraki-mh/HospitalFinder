using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Core.DataAccess
{
    public interface IHospitalRepository : IRepository<Hospital>
    {
        Task<List<Hospital>> GetHospitalsInRange(double xMin, double xMax, double yMin, double yMax);
    }
}