using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;
using Marten;

namespace ContractManagement.Application.Query.GetItemOrderById
{
    internal sealed class GetItemOrderByIdQueryHandler(
        // IPedidoItemRepository itemRepository,
        IQuerySession session) : IQueryHandler<GetItemOrderByIdQuery, ItemOrderResponse>
    {
        //private readonly IPedidoItemRepository _itemRepository = itemRepository;
        private readonly IQuerySession _session = session;

        public async Task<Result<ItemOrderResponse>> Handle(GetItemOrderByIdQuery request, CancellationToken cancellationToken)
        {
            //var itemOrder = await _itemRepository.GetItemPedidoByIdAsync(request.ItemOrder, cancellationToken);
            var itemOrder = await _session.Query<ItemPedido>().Select(p => new ItemOrderResponse(p.IdProduto, p.Produto, p.Quantidade, p.PrecoUnitario)).FirstOrDefaultAsync(cancellationToken);

            if (itemOrder is null)
            {
                return Result.Failure<ItemOrderResponse>(new Error(
                    "ItemOrder.NotFound",
                    $"The member with id {request.ItemOrder} was not found"
                    ));
            }
            return itemOrder;
        }
    }
}
