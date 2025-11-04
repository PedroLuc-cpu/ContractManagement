using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Product.Query
{
    public sealed record GetAllProductQuery : IQuery<IEnumerable<GetProductResponse>>
    {
    }
}
