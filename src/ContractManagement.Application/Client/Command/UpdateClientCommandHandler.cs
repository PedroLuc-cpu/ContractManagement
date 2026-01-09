using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Client.Command
{
    internal sealed class UpdateClientCommandHandler(IClienteRepository clientRespository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateClientCommand>
    {
        private readonly IClienteRepository _clienteRepository = clientRespository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {

            var clientExist = await _clienteRepository.GetByIdAsync(request.IdCliente,  cancellationToken);
            if (clientExist is null)
            {
                return Result.Failure(new Error("ClientNotFound", $"Não foi encontrado nenhum cliente com IdCliente {request.IdCliente} informado"));
            }

            clientExist.Update(request.Nome, request.SobreNome, request.Email);
            
            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
