using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Client.Command
{
    public sealed record UpdateClientCommand(Guid IdCliente, string Nome, string SobreNome, string Email, string Rua, string Numero, string Cidade, string Estado, string Cep) : ICommand
    {
    }
}
