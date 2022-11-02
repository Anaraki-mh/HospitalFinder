using HospitalFinder.Core.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Core.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Properties and fields

        private HospitalFinderContext _context { get; }

        #endregion


        #region Constructor

        public Repository(HospitalFinderContext hospitalFinderContext)
        {
            _context = hospitalFinderContext;
        }

        #endregion


        #region Methods

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public List<T> List()
        {
            return _context.Set<T>().ToList();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        #endregion
    }
}
