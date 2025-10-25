using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Query.Order
{
    internal sealed class GetOrdersQueryHandler(IPedidoRepository pedidoRepository) : IQueryHandler<GetOrdersQuery, List<GetOrdersQueryResponse>>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        public async Task<Result<List<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<GetOrdersQueryResponse> pedido = await _pedidoRepository.ListaPaginada(1, 100, cancellationToken)
                .ContinueWith(pedidos => pedidos.Result.Select(p => new GetOrdersQueryResponse();
                   
        }
    }
}
