using HospitalFinder.Core.DataAccess;
using HospitalFinder.Domain.HospitalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Services
{
    public class HospitalService : IHospitalService
    {
        #region Fields and Properties

        private IRepository<Hospital> _repository { get; }

        #endregion


        #region Constructor

        public HospitalService(IRepository<Hospital> repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods
        public Hospital Create(Hospital entity)
        {
            return _repository.Create(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(FindById(id));
        }

        public Hospital? FindById(int id)
        {
            return _repository.List()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<Hospital> List()
        {
            return _repository.List();
        }

        public void Update(Hospital entity)
        {
            _repository.Update(entity);
        }

        public List<Hospital> Search(string keyword, int pageNumber, int numberOfResultsPerPage)
        {
            return List().Where(x => x.Name.Contains(keyword) || x.City.Contains(keyword) || x.Country.Contains(keyword))
                .Skip((pageNumber - 1) * numberOfResultsPerPage)
                .Take(numberOfResultsPerPage)
                .ToList();
        }

        public List<Hospital> FindNearest(double latitude, double longtitude, int numberOfResults)
        {
            return List().OrderBy(x =>
            (latitude - x.Latitude) * (latitude - x.Latitude) +
            (longtitude - x.Longtitude) * (longtitude - x.Longtitude))
                .Take(numberOfResults)
                .ToList();
        }


        #endregion
    }
}
