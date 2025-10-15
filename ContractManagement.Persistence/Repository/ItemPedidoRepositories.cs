using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository
{
    public class ItemPedidoRepositories(ContractManagementContext context) : IPedidoItemRepository
    {
        private readonly ContractManagementContext _context = context;
        public async Task AdicionarItemPedidoAsync(ItemPedidoEntity itemPedido)
        {
            await _context.ItemPedido.AddAsync(itemPedido);
        }

        public async Task<IEnumerable<PedidoEntity>> GetAllItemsPedidosAsync(Guid pedidoId)
        {
            return (IEnumerable<PedidoEntity>)await _context.ItemPedido.Where(i => EF.Property<Guid>(i, "PedidoId") == pedidoId).AsNoTracking().ToListAsync();
            
        }        
        public async Task<ItemPedidoEntity?> GetItemPedidoByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.ItemPedido.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
        }

        public async Task RemoveAsync(Guid id)
        {
            var item = await _context.ItemPedido.FindAsync(new object[] {id });
            if (item != null)
            {
                _context.Remove(item);
            }
        }
    }
}
