using ContractManagement.Domain.Interfaces;
using ContractManagement.Infrastructure.Persistence;

namespace ContractManagement.Persistence
{
    public sealed class UnitOfWork(ContractManagementContext context) : IUnitOfWork
    {
        private readonly ContractManagementContext _context = context;

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                Console.WriteLine($"{entry.Entity.GetType().Name} - {entry.State}");
            }

            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
