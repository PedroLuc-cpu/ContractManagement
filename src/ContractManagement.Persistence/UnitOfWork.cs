using ContractManagement.Domain.Interfaces;

namespace ContractManagement.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
