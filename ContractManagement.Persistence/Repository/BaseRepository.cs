using ContractManagement.Domain.Primitives;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class BaseRepository<T>(ContractManagementContext context) : IBaseRepository<T> where T : class, IAggregateRoot
    {
        protected readonly ContractManagementContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task DeleteAsync(T entity) => await _dbSet.Where(b => b.Equals(entity)).ExecuteDeleteAsync();


        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        

        public async Task InsertAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task UpdateAsync(T entity) =>
            await _dbSet.Where(b => b.Equals(entity)).ExecuteUpdateAsync(
                setters => setters.SetProperty(b => b.Equals(entity), entity.Equals(entity)));
    }
}
