using ContractManagement.Application.Interfaces;

namespace ContractManagement.Infrastructure.Persistence
{
    public class UnitOfWork(ContractManagementContext context) : IUnitOfWork
    {
        private readonly ContractManagementContext _context = context;
        public async Task<bool> Commit()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
