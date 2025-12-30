using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Solicitacao;

namespace ContractManagement.Application.RequestInternal.Command
{
    public sealed record CreateRequestInternalCommand(Guid IdFuncionario, Periodo? Periodo, string Titulo, string Descricao) : ICommand
    {
    }
}
