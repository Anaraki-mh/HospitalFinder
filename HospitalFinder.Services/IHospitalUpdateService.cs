using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Services
{
    public interface IHospitalUpdateService
    {
        Task<HospitalUpdate> CreateAsync(HospitalUpdate entity);
        Task DeleteAsync(int id);
        Task RemoveAsync(int id);
        Task<HospitalUpdate>? FindByIdAsync(int id);
        Task<List<HospitalUpdate>> ListAsync();
        Task UpdateAsync(HospitalUpdate entity);
    }
}