using ContractManagement.Domain.Shared;
using MediatR;

namespace ContractManagement.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
