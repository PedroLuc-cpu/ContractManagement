using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public sealed record PagamentoInciadoEvent(Guid Id, decimal ValorTotal) : IDomainEvent
    {
    }
}