using System;
using WebAPI.Entity;

namespace WebAPI.Repository
{
    public class UnitOfWork : IDisposable
    {
        private MyEntities dbContext;
        private GenericRepository<Category> categoryRepository;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        public UnitOfWork(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }

        protected MyEntities DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        public GenericRepository<Category> CategoryRepository
        {
            get
            {
                if (this.categoryRepository == null)
                {
                    this.categoryRepository = new GenericRepository<Category>(DbFactory);
                }

                return categoryRepository;
            }
        }

        public bool Save()
        {
            return DbContext.SaveChanges() > 0;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
