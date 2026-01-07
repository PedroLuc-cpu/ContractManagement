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
            var orders = await _dbSet
                .Include(item => item.Items)
                .OrderBy(d => d.DataCriacao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return orders;
        }

        public async Task<Pedido?> ObterPedidoPorId(Guid Id, CancellationToken cancellationToken = default)
        {
            var pedido = await _context.Pedidos.Include(item => item.Items).FirstOrDefaultAsync(i => i.Id == Id, cancellationToken);
            return pedido;
        }
    }
}
