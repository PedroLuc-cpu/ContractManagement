using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.RequestInternal.Query
{
    internal sealed class GetRequestByIdQueryHandler(ISolicitacaoInternaRepository solicitacaoInternaRepository) : IQueryHandler<GetRequestByIdQuery, GetRequestInternalResponse>
    {
        private readonly ISolicitacaoInternaRepository _solicitacaoInternaRepository = solicitacaoInternaRepository;
        public async Task<Result<GetRequestInternalResponse>> Handle(GetRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var solicitacao = await _solicitacaoInternaRepository.GetByIdAsync(request.Id, cancellationToken);

            if (solicitacao is null)
            {
                return (Result<GetRequestInternalResponse>)Result.Failure(new Error("GetRequestByIdQueryHandler", "Não foi encontrado nenhum um solicitação"));
            }

            var response = new GetRequestInternalResponse(
                solicitacao.Id,
                solicitacao.IdFuncionario,
                solicitacao.DataCriacao,
                solicitacao.Periodo,
                solicitacao.Titulo,
                solicitacao.Descricao,
                solicitacao.Historico,
                solicitacao.Status
            );

            return Result.Success( response );
        }
    }
}
