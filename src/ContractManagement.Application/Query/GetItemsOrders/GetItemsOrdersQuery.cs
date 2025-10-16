using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Query.GetItemsOrders
{
    public sealed record GetItemsOrdersQuery() : IQuery<List<ItemsOrdersResponse>>
    {
    }
}
