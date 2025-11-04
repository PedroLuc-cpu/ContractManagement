using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Client.Command
{
    internal sealed class UpdateClientCommandHandler(IClienteRepository clientRespository) : ICommandHandler<UpdateClientCommand>
    {
        private readonly IClienteRepository _clienteRepository = clientRespository;
        public async Task<Result> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {

            var clientExist = await _clienteRepository.GetByEmailAsync(request.Email,  cancellationToken);
            if (clientExist is null)
            {
                return Result.Failure(new Error("ClientNotFound", "Não foi encontrado nenhum cliente com email informado"));
            }

            clientExist.Update(request.FirstName, request.LastName, request.Email);
            clientExist.UpdateAddress(request.Street, request.Number, request.City, request.State, request.ZipCode);
            
                await _clienteRepository.UpdateClientAsync(clientExist, cancellationToken);

            return Result.Success();
        }
    }
}
