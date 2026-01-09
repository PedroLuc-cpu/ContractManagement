using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Events.Pedidos
{
    public sealed record PedidoCriadoEvent(Guid IdPedido, IReadOnlyCollection<ItemPedidoSnapshot> Itens) : IDomainEvent
    {
    }
}
