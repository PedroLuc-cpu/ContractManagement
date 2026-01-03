using ContractManagement.Domain.Common.Base;

namespace ContractManagement.Domain.Primitives
{
    public abstract class AggregateRoot(Guid id, DateTime dataCriacao) : EntityBase(id, dataCriacao)
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
