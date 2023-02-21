using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Services
{
    public interface IHospitalService
    {
        Task<Hospital> CreateAsync(Hospital entity);
        Task DeleteAsync(int id);
        Task<List<Hospital>> ListAsync();
        Task UpdateAsync(Hospital entity);
        Task<Hospital>? FindByIdAsync(int id);
        Task<List<Hospital>> SearchAsync(string keyword, int pageNumber, int numberOfResultsPerPage);
        Task<List<Hospital>> FindNearestAsync(double latitude, double longtitude, int numberOfResults);
    }
}