using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Query.GetItemOrderById
{
    internal sealed class GetItemOrderByIdQueryHandler : IQueryHandler<GetItemOrderByIdQuery, ItemOrderResponse>
    {
        private readonly IPedidoItemRepository _itemRepository;
        public GetItemOrderByIdQueryHandler(IPedidoItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<ItemOrderResponse>> Handle(GetItemOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var itemOrder = await _itemRepository.GetItemPedidoByIdAsync(request.ItemOrder, cancellationToken);

            if (itemOrder is null)
            {
                return Result.Failure<ItemOrderResponse>(new Error(
                    "ItemOrder.NotFound",
                    $"The member with id {request.ItemOrder} was not found"
                    ));
            }

            var response = new ItemOrderResponse(itemOrder.Id, itemOrder.Produto, itemOrder.Quantidade, itemOrder.PrecoUnitario);
            return response;
        }
    }
}
