using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Solicitacao;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.RequestInternal.Command
{
    internal sealed class CreateRequestInternalCommandHandler(ISolicitacaoInternaRepository solicitacaoInternaRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateRequestInternalCommand>
    {
        private readonly ISolicitacaoInternaRepository _solicitacaoInternaRepository = solicitacaoInternaRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(CreateRequestInternalCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = new SolicitacaoInterna(request.IdFuncionario, request.Periodo, request.Titulo, request.Descricao);

            await _solicitacaoInternaRepository.Add(solicitacao, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);         

            return Result.Success(solicitacao.Id);
        }
    }
}
