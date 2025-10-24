using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class PedidoRepositories(ContractManagementContext context) : BaseRepository<Pedido>(context), IPedidoRepository
    {
        public async Task Adicionar(Pedido pedido, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(pedido, cancellationToken);
        }

        public async Task<IEnumerable<Pedido>> ListaPaginada(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var orders = await _dbSet.AsNoTracking()
                .Include(item => item.Items)
                .OrderBy(d => d.DataCriacao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return orders;
        }

        public async Task<Pedido> ListarComItens(Guid id)
        {
            
            var pedidoItems = await _dbSet
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedidoItems is not null)
            {
                return pedidoItems;
            }
            throw new KeyNotFoundException("Pedido não encontrado");
        }
    }
}
