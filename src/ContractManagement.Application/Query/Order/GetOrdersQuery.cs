using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Query.Order
{
    public sealed record GetOrdersQuery: IQuery<IEnumerable<GetOrdersQueryResponse>>
    {
    }
}
