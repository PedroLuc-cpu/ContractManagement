using MediatR;

namespace ContractManagement.Application.Product
{
    internal sealed class ProdutoIndisponibilizadoDomainEventHandler : INotificationHandler<ProdutoIndisponibilizadoDomainEvent>
    {
        public Task Handle(ProdutoIndisponibilizadoDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
