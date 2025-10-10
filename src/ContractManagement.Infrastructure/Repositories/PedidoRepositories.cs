using ContractManagement.Application.Interfaces.Repository.IPedido;
using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Infrastructure.Repositories
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
