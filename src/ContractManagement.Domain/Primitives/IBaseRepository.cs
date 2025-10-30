namespace ContractManagement.Domain.Primitives
{
    /// <summary>
    /// Interface genérica para repositórios de agregados.
    /// Apenas entidades que implementam IAggregateRoot podem ser usadas.
    /// </summary>
    public interface IBaseRepository<T> where T : AggregateRoot
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
