namespace ContractManagement.Domain.Interfaces
{
    /// <summary>
    /// Unidade de trabalho para coordenar transações.
    /// </summary>
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
