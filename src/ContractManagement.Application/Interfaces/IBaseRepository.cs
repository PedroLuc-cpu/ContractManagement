using ContractManagement.Domain.Interfaces;

namespace ContractManagement.Application.Interfaces
{
    /// <summary>
    /// Interface genérica para repositórios de agregados.
    /// Apenas entidades que implementam IAggregateRoot podem ser usadas.
    /// </summary>
    public interface IBaseRepository<T> where T : IAggregateRoot
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
