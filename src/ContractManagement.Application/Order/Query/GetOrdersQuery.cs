using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Order.Query
{
    public sealed record GetOrdersQuery: IQuery<IEnumerable<GetOrdersQueryResponse>>
    {
    }
}
