using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Primitives;
using ContractManagement.Infrastructure.Persistence;
using MediatR;

namespace ContractManagement.Persistence
{
    public sealed class UnitOfWork(ContractManagementContext context, IMediator mediator) : IUnitOfWork
    {
        private readonly ContractManagementContext _context = context;
        private readonly IMediator _mediator = mediator;


        public async Task Commit(CancellationToken cancellationToken = default)
        {
            var domainEvents = _context.ChangeTracker.Entries<AggregateRoot>().SelectMany(e => e.Entity.GetDomainEvents()).ToList();

            Console.WriteLine($"[UOW] Eventos coletados: {domainEvents.Count}");      

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in domainEvents)
            {
                Console.WriteLine($"[UOW] Publicando evento: {domainEvent.GetType().Name}");

                await _mediator.Publish(domainEvent, cancellationToken);
            }

            foreach (var entry in _context.ChangeTracker.Entries<AggregateRoot>())
            {
                entry.Entity.ClearDomainEvents();
                Console.WriteLine("[UOW] Eventos limpos");
            }
        }
    }
}
