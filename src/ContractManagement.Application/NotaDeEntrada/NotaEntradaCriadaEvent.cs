using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Application.NotaDeEntrada
{
    public sealed record NotaEntradaCriadaEvent(Guid IdNotaEntrada, IReadOnlyCollection<NotaEntradaItemSnapshot> Itens) : IDomainEvent
    {
    }
}
