using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Events
{
    public sealed record PedidoCriadoEvent(Guid IdPedido, IReadOnlyCollection<ItemPedidoSnapshot> Itens) : IDomainEvent
    {
    }
}
