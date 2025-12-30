using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.RequestInternal.Command
{
    internal sealed class RejectRequestInternalCommandHandler(ISolicitacaoInternaRepository solicitacaoInternaRepository, IUnitOfWork unitOfWork) : ICommandHandler<RejectRequestInternalCommand>
    {
        private readonly ISolicitacaoInternaRepository _solicitacaoInternaRepository = solicitacaoInternaRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(RejectRequestInternalCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = await _solicitacaoInternaRepository.GetByIdAsync(request.Id, cancellationToken);
            if (solicitacao is null)
            {
                return Result.Failure(new Error("RejectRequestInternalCommandHandler", "Não foi encontrado nenhum um solicitação"));
            }

            solicitacao.Rejeitar(request.Comentario);

            await _unitOfWork.Commit(cancellationToken);
            return Result.Success(solicitacao);
        }
    }
}
