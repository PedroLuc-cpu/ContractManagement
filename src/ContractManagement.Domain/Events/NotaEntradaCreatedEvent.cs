using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Events
{
    public sealed class NotaEntradaCreatedEvent(Guid notaEntradaId, IEnumerable<NotaEntradaItemSnapshot> itens) : IDomainEvent
    {
        public Guid NotaEntradaId { get; } = notaEntradaId;
        public IEnumerable<NotaEntradaItemSnapshot> Itens { get; } = itens;
    }
}