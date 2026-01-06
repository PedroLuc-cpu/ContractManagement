using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.NotaDeEntrada
{
    internal sealed class RegistrarNotaEntradaCommandHandler(INotaEntradaRepository notaEntradaRepository, IUnitOfWork unitOfWork) : ICommandHandler<RegistrarNotaEntradaCommand>
    {
        private readonly INotaEntradaRepository _notaEntradaRepository = notaEntradaRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result> Handle(RegistrarNotaEntradaCommand request, CancellationToken cancellationToken)
        {
            var nota = NotaEntrada.Create(request.NumeroDocumento, request.Itens);

            await _notaEntradaRepository.Adicionar(nota, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();

        }
    }
}
