using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Enums;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Order.Query
{
    internal sealed class GetOrdersQueryHandler(IPedidoRepository pedidoRepository) : IQueryHandler<GetOrdersQuery, IEnumerable<GetOrdersQueryResponse>>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;

        public async Task<Result<IEnumerable<GetOrdersQueryResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {

            var orders = await _pedidoRepository.ListaPaginada(request.PageNumber, request.PageSize, cancellationToken);
                

            var response = orders.Select(o => new GetOrdersQueryResponse(
                o.Id,
                o.IdCliente,
                StatusPedidoEnumExtensions.GetDescription(o.Status),
                o.DataCriacao,
                o.ValorTotal.Value,
                [.. o.Items.Select(i => new GetOrdersQueryResponseItemOrder(
                        i.Id,
                        i.NomeProduto,
                        i.Quantidade,
                        i.PrecoUnitario.Value,
                        i.Total.Value
                    ))]
            ));

            return Result.Success<IEnumerable<GetOrdersQueryResponse>>(response);
        }
    }
}
