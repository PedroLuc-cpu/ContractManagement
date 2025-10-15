using ContractManagement.Domain.Shared;
using MediatR;

namespace ContractManagement.Application.Abstractions.Messaging
{
    internal interface IQueryHandler<TQuery, TRespose> : IRequestHandler<TQuery, Result<TRespose>> where TQuery : IQuery<TRespose>
    {
    }
}
