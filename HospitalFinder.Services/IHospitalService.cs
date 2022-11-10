using HospitalFinder.Domain.HospitalData;

namespace HospitalFinder.Services
{
    public interface IHospitalService
    {
        Hospital Create(Hospital entity);
        void Delete(int id);
        List<Hospital> List();
        void Update(Hospital entity);
        Hospital? FindById(int id);
        List<Hospital> Search(string keyword, int pageNumber, int numberOfResultsPerPage);
        List<Hospital> FindNearest(double latitude, double longtitude, int numberOfResults);
    }
}