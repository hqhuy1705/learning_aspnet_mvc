using System;
using WebAPI.Entity;

namespace WebAPI.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Category> CategoryRepository { get; }
        bool Save();
    }
}
