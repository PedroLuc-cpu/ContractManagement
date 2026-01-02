using ContractManagement.Domain.Primitives;

namespace ContractManagement.Application.Order.Event
{
    public sealed record PedidoCriadoEvent(Guid IdPedido, IReadOnlyCollection<ItemPedidoSnapshot> Itens) : IDomainEvent
    {
    }
}
