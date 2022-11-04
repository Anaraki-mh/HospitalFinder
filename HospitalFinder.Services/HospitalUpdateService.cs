using HospitalFinder.Core.DataAccess;
using HospitalFinder.Domain.HospitalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Services
{
    public class HospitalUpdateService : IHospitalUpdateService
    {
        #region Fields and Properties

        private IRepository<HospitalUpdate> _repository { get; }

        #endregion


        #region Constructor

        public HospitalUpdateService(IRepository<HospitalUpdate> repository)
        {
            _repository = repository;
        }

        #endregion


        #region Methods
        public HospitalUpdate Create(HospitalUpdate entity)
        {
            return _repository.Create(entity);
        }

        public void Delete(int id)
        {
            _repository.Delete(FindById(id));
        }

        public HospitalUpdate? FindById(int id)
        {
            return _repository.List()
                .FirstOrDefault(x => x.Id == id);
        }

        public List<HospitalUpdate> List()
        {
            return _repository.List();
        }

        public void Update(HospitalUpdate entity)
        {
            _repository.Update(entity);
        }

        #endregion
    }
}
