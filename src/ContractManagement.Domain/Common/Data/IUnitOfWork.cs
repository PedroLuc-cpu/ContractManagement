namespace ContractManagement.Domain.Common.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
