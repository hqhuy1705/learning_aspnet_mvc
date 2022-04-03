using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(object id);
        void Insert(T entity);
        void Inserts(IEnumerable<T> entites);
        void Update(T obj);
        void Delete(object id);
        bool Save();
    }
}
