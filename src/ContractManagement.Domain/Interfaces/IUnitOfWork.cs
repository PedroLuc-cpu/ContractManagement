namespace ContractManagement.Domain.Interfaces
{
    /// <summary>
    /// Unidade de trabalho para coordenar transações.
    /// </summary>
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken = default);
    }
}
