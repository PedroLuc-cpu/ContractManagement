using ContractManagement.Domain.Shared;
using MediatR;

namespace ContractManagement.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result> { }
    public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
        
}
