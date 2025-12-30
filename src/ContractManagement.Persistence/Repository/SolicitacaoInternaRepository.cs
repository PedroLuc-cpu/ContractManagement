using ContractManagement.Domain.Entity.Solicitacao;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    internal sealed class SolicitacaoInternaRepository(ContractManagementContext context) : ISolicitacaoInternaRepository
    {
        private readonly ContractManagementContext _context = context;

        public async Task Add(SolicitacaoInterna solicitacaoInterna, CancellationToken cancellationToken = default)
        {
            await _context.SolicitacaoInternas.AddAsync(solicitacaoInterna, cancellationToken);
        }

        public async Task DeleteAsync(SolicitacaoInterna entity, CancellationToken cancellationToken = default)
        {
           await _context.SolicitacaoInternas.Where(x => x.Id == entity.Id).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<IEnumerable<SolicitacaoInterna>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SolicitacaoInternas.AsNoTracking().Include(x => x.Historico).ToListAsync(cancellationToken);
        }
        public async Task<SolicitacaoInterna?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.SolicitacaoInternas
                                    .Include(s => s.Historico)
                                    .SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }
    }
}
