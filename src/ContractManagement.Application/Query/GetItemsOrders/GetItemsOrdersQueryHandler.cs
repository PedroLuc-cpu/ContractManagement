using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;
using Marten;

namespace ContractManagement.Application.Query.GetItemsOrders
{
    internal sealed class GetItemsOrdersQueryHandler(IQuerySession session) : IQueryHandler<GetItemsOrdersQuery, List<ItemsOrdersResponse>>
    {
        private readonly IQuerySession _session = session;

        public async Task<Result<List<ItemsOrdersResponse>>> Handle(GetItemsOrdersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<ItemsOrdersResponse> ítensOrders = 
                await _session
                .Query<ItemPedido>()
                .Select(i => new ItemsOrdersResponse(i.IdProduto, i.Produto, i.Quantidade, i.PrecoUnitario))
                .ToListAsync(cancellationToken) ;

            return ítensOrders.ToList();
        }
    }
}
