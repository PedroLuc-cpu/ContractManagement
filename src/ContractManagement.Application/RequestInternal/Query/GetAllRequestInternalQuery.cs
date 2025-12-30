using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.RequestInternal.Query
{
    public sealed record GetAllRequestInternalQuery : IQuery<IEnumerable<GetRequestInternalResponse>>
    {
    }
}
