using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Interfaces.Repository.Pedidos
{
    public interface IPedidoItemRepository
    {
        Task AdicionarItemPedidoAsync(ItemPedido itemPedido);
        Task<ItemPedido?> GetItemPedidoByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pedido>> GetAllItemsPedidosAsync(Guid pedidoId);
        Task RemoveAsync(Guid id);
    }
}
