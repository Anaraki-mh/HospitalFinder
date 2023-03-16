using HospitalFinder.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace HospitalFinder.Core.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties and fields

        private protected HospitalFinderContext _context { get; }

        #endregion


        #region Constructor

        public Repository(HospitalFinderContext hospitalFinderContext)
        {
            _context = hospitalFinderContext;
        }

        #endregion


        #region Methods

        public async Task<T> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
