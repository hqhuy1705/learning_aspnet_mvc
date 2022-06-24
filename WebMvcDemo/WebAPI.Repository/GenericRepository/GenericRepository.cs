using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebAPI.Entity;
using WebAPI.Repository.Extensions;

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

        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Inserts(IEnumerable<T> entites)
        {
            dbSet.AddRange(entites);
        }

        public virtual void Update(T obj)
        {
            dbSet.Attach(obj);
            DbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T existing = dbSet.Find(id);
            dbSet.Remove(existing);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }


        public virtual bool Save()
        {
            return DbContext.SaveChanges() > 0;
        }

        public virtual IEnumerable<T> ExecuteStoredProcedure(string storedProcedure, object parameters = null)
        {
            return DbContext.Database.SqlQuerySmart<T>(storedProcedure, parameters);
        }

        public virtual int ExecuteSqlCommandSmart(string storedProcedure, object parameters = null)
        {
            return DbContext.Database.ExecuteSqlCommandSmart(storedProcedure, parameters);
        }
    }
}
