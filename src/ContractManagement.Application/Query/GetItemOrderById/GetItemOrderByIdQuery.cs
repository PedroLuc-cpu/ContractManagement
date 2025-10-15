using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Query.GetItemOrderById
{
    public sealed record GetItemOrderByIdQuery(Guid ItemOrder) : IQuery<ItemOrderResponse>
    {
    }

}
