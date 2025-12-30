using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.RequestInternal.Query
{
    internal sealed class GetAllRequestInternalQueryHandler(ISolicitacaoInternaRepository solicitacaoInternaRepository) : IQueryHandler<GetAllRequestInternalQuery, IEnumerable<GetRequestInternalResponse>>
    {
        private readonly ISolicitacaoInternaRepository _solicitacaoInternaRepository = solicitacaoInternaRepository;
        public async Task<Result<IEnumerable<GetRequestInternalResponse>>> Handle(GetAllRequestInternalQuery request, CancellationToken cancellationToken)
        {
            var requestInternal = await _solicitacaoInternaRepository.GetAllAsync(cancellationToken);
            var response = requestInternal.Select(x => new GetRequestInternalResponse(
                x.Id,
                x.IdFuncionario,
                x.DataCriacao,
                x.Periodo,
                x.Titulo,
                x.Descricao,
                x.Historico,
                x.Status));

            return Result.Success<IEnumerable<GetRequestInternalResponse>>(response);
        }
    }
}
