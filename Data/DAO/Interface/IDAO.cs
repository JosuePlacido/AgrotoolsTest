using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Agrotools.Data.DAO
{
    public interface IDAO<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Add(TEntity obj);
        Task<TEntity> Update(TEntity obj);
        Task<TEntity> Delete(TEntity obj);
        Task<TEntity> GetById(string id);
        Task<TEntity[]> GetAll();
        Task<IDbContextTransaction> BeginTtransaction();
    }
}
