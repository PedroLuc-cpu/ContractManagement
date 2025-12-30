using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.RequestInternal.Command
{
    internal sealed class EditRequestInternalCommandHandler(ISolicitacaoInternaRepository solicitacaoInternaRepository, IUnitOfWork unitOfWork) : ICommandHandler<EditRequestInternalCommand>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ISolicitacaoInternaRepository _solicitacaoInternaRepository = solicitacaoInternaRepository;
        
        public async Task<Result> Handle(EditRequestInternalCommand request, CancellationToken cancellationToken)
        {
            var solicitacao = await _solicitacaoInternaRepository.GetByIdAsync(request.Id, cancellationToken);

            if (solicitacao is null)
            {
                return Result.Failure(new Error("EditRequestInternalCommand", "Nâo foi encontrado nenhuma solitacação fornecida"));
            }

            solicitacao.Editar(request.Titulo, request.Descricao, request.Periodo, request.Comentario);

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success(solicitacao.Id);
            
        }
    }
}
