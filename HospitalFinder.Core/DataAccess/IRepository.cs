using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.Core.DataAccess
{
    public interface IRepository<T> where T : class
    {
        List<T> List();
        T Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
