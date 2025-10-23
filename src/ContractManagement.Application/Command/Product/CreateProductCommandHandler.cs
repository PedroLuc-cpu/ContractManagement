using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Shared;
using Marten;

namespace ContractManagement.Application.Command.Product
{
    internal sealed class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand>
    {
        private readonly IDocumentSession _session = session;

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ItemPedido(Guid.NewGuid(), request.ProductName, request.Quantity, request.UnitPrice);
            _session.Store(product);

            await _session.SaveChangesAsync(cancellationToken);

            return Result.Success();
            
        }
    }
}
