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
        public async Task<HospitalUpdate> CreateAsync(HospitalUpdate entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(await FindByIdAsync(id));
        }
        public async Task RemoveAsync(int id)
        {
            HospitalUpdate entity = await FindByIdAsync(id);
            if (entity != null)
            {
                entity.IsRemoved = true;
                await _repository.UpdateAsync(entity);
            }
        }

        public async Task<HospitalUpdate>? FindByIdAsync(int id)
        {
            List<HospitalUpdate> entityList = await ListAsync();

            return entityList?.FirstOrDefault(x => x.Id == id) ?? new HospitalUpdate();
        }

        public async Task<List<HospitalUpdate>> ListAsync()
        {
            List<HospitalUpdate> entityList = await _repository.ListAsync();

            return entityList.Where(x => !x.IsRemoved).ToList() ?? new List<HospitalUpdate>();
        }

        public async Task UpdateAsync(HospitalUpdate entity)
        {
            await _repository.UpdateAsync(entity);
        }

        #endregion
    }
}
