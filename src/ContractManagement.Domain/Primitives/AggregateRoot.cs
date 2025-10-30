using ContractManagement.Domain.Common.Base;

namespace ContractManagement.Domain.Primitives
{
    public abstract class AggregateRoot(Guid id) : EntityBase(id)
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
    }
}
