using ContractManagement.Domain.Primitives;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class BaseRepository<T>(ContractManagementContext context) : IBaseRepository<T> where T : AggregateRoot
    {
        protected readonly ContractManagementContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default) => await _dbSet.Where(b => b.Equals(entity)).ExecuteDeleteAsync();

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken) => await _dbSet.ToListAsync(cancellationToken);

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _dbSet.FirstOrDefaultAsync(b => EF.Property<Guid>(b, "Id") == id, cancellationToken);

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default) =>
            await _dbSet.Where(b => b.Equals(entity)).ExecuteUpdateAsync(
                setters => setters.SetProperty(b => b.Equals(entity), entity.Equals(entity)), cancellationToken);
    }
}
