using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Client.Command
{
    internal sealed class CreateClientCommandHandler(IClienteRepository clienteRepository) : ICommandHandler<CreateClientCommand>
    {
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        public async Task<Result> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var clienteExist = await _clienteRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (clienteExist is not null)
            {
                return Result.Failure(new Error("ClientEmailExist", "O cliente quer já se encontra com email cadastrado."));
            }
            var cliente = Cliente.Create(request.FirstName, request.LastName, request.Email, request.Street, request.Number, request.City, request.State, request.ZipCode);
                                  
            await _clienteRepository.CreateClientAsync(cliente, cancellationToken);

            return Result.Success(cliente);
        }
    }
}
