using ContractManagement.Domain.Primitives;

namespace ContractManagement.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        public Task Commit(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
