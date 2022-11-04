using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Services
{
    public interface IHospitalService
    {
        Hospital Create(Hospital entity);
        void Delete(int id);
        Hospital? FindById(int id);
        List<Hospital> List();
        void Update(Hospital entity);
    }
}