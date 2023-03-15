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
        public async Task<Hospital> CreateAsync(Hospital entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(await FindByIdAsync(id));
        }

        public async Task<Hospital>? FindByIdAsync(int id)
        {
            List<Hospital> entityList = await _repository.ListAsync();
            return entityList?.FirstOrDefault(x => x.Id == id) ?? new Hospital();
        }

        public async Task<List<Hospital>> ListAsync()
        {
            return await _repository.ListAsync();
        }

        public async Task UpdateAsync(Hospital entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task<List<Hospital>> SearchAsync(string keyword, int numberOfResults)
        {
            List<Hospital> entityList = await ListAsync();

            return entityList.Where(x => x.Name.Contains(keyword) || x.City.Contains(keyword) || x.Country.Contains(keyword))
           .Take(numberOfResults)
           .ToList();
        }

        public async Task<List<Hospital>> FindNearestAsync(double latitude, double longtitude, int numberOfResults)
        {
            List<Hospital> entityList = await ListAsync();

            return entityList.OrderBy(x =>
            (latitude - x.Latitude) * (latitude - x.Latitude) +
            (longtitude - x.Longtitude) * (longtitude - x.Longtitude))
                .Take(numberOfResults)
                .ToList();
        }


        #endregion
    }
}
