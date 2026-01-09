using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Events.Pagamentos
{
    public sealed record PagamentoConfirmadoEvent(Guid Id, decimal ValorTotal) : IDomainEvent
    {
    }
}