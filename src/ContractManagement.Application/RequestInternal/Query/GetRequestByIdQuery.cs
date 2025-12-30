using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.RequestInternal.Query
{
    public sealed record GetRequestByIdQuery(Guid Id) : IQuery<GetRequestInternalResponse>
    {
    }
}
