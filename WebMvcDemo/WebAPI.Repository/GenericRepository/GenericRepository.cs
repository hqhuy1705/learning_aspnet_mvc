using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Entity;

namespace WebAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyEntities dbContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected MyEntities DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Inserts(IEnumerable<T> entites)
        {
            dbSet.AddRange(entites);
        }

        public void Update(T obj)
        {
            dbSet.Attach(obj);
            dbContext.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }

        public bool Save()
        {
            return dbContext.SaveChanges() > 0;
        }
    }
}
