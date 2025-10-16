using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Domain.Shared;
using Marten;

namespace ContractManagement.Application.Query.GetItemsOrders
{
    internal sealed class GetItemsOrdersQueryHandler : IQueryHandler<GetItemsOrdersQuery, List<ItemsOrdersResponse>>
    {
        private readonly IQuerySession _session;
        public GetItemsOrdersQueryHandler(IQuerySession session) {  _session = session; }
        public async Task<Result<List<ItemsOrdersResponse>>> Handle(GetItemsOrdersQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<ItemsOrdersResponse> ítensOrders = 
                await _session
                .Query<ItemPedidoEntity>()
                .Select(i => new ItemsOrdersResponse(i.Id, i.Produto, i.Quantidade, i.PrecoUnitario))
                .ToListAsync(cancellationToken) ;

            return ítensOrders.ToList();
        }
    }
}
