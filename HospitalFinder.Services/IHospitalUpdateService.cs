using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Services
{
    public interface IHospitalUpdateService
    {
        HospitalUpdate Create(HospitalUpdate entity);
        void Delete(int id);
        HospitalUpdate? FindById(int id);
        List<HospitalUpdate> List();
        void Update(HospitalUpdate entity);
    }
}