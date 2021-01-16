using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Agrotools.Data.DAO
{
    public abstract class DAOBase<TEntity> : IDAO<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _context;
        public DAOBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<TEntity> Add(TEntity obj)
        {
            await _context.Set<TEntity>().AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<TEntity> Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<TEntity> Delete(TEntity obj)
        {
            _context.Set<TEntity>().Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<IDbContextTransaction> BeginTtransaction()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity[]> GetAll()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToArrayAsync();
        }
    }
}
