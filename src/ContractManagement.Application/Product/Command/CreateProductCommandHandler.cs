using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;
using Marten;

namespace ContractManagement.Application.Product.Command
{
    internal sealed class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand>
    {
        private readonly IDocumentSession _session = session;

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ItemPedido(Guid.NewGuid(), request.ProductName, request.Quantity, Money.Create(request.UnitPrice).Value);
            _session.Store(product);

            await _session.SaveChangesAsync(cancellationToken);

            return Result.Success();
            
        }
    }
}
