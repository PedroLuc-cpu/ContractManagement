using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Order.Query
{
    internal sealed class GetOrdersQueryHandler(IPedidoRepository pedidoRepository) : IQueryHandler<GetOrdersQuery, IEnumerable<GetOrdersQueryResponse>>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public Task<Result<IEnumerable<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
