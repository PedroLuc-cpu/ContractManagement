using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    internal sealed class EstoqueRepository(ContractManagementContext context) : IEstoqueRepository
    {
        private readonly ContractManagementContext _context = context;
        public async Task Adicionar(Estoque estoque, CancellationToken cancellationToken)
        {
            await _context.Estoque.AddAsync(estoque, cancellationToken);
        }

        public async Task<Estoque?> ObterProdutoId(Guid idProduto, CancellationToken cancellationToken)
        {
            return await _context.Estoque.SingleOrDefaultAsync(e => e.IdProduto == idProduto, cancellationToken);
        }
    }
}
