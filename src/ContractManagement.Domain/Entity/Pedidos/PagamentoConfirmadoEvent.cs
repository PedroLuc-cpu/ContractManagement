using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public sealed record PagamentoConfirmadoEvent(Guid Id, decimal ValorTotal) : IDomainEvent
    {
    }
}