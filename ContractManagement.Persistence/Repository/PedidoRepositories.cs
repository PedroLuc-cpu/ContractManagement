using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class PedidoRepositories(ContractManagementContext context) : BaseRepository<PedidoEntity>(context), IPedidoRepository
    {
        public async Task<PedidoEntity> ListarComItens(Guid id)
        {
            
            var pedidoItems = await _dbSet
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);

            return pedidoItems;            
        }
    }
}
