using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Order.Query
{
    public sealed record GetOrdersQuery(int PageNumber, int PageSize) : IQuery<IEnumerable<GetOrdersQueryResponse>>
    {
    }
}
