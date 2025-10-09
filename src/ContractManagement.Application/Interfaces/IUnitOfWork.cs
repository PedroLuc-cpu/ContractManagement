namespace ContractManagement.Application.Interfaces
{
    /// <summary>
    /// Unidade de trabalho para coordenar transações.
    /// </summary>
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
