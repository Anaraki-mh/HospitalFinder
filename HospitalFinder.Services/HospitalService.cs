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

        #endregion
    }
}
