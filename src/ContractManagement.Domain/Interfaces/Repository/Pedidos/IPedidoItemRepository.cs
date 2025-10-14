using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Domain.Interfaces.Repository.Pedidos
{
    public interface IPedidoItemRepository
    {
        Task AdicionarItemPedidoAsync(ItemPedidoEntity itemPedido);
        Task<ItemPedidoEntity> GetItemPedidoByIdAsync(Guid id);
        Task<IEnumerable<PedidoEntity>> GetAllItemsPedidosAsync(Guid pedidoId);
        Task RemoveAsync(Guid id);
    }
}
