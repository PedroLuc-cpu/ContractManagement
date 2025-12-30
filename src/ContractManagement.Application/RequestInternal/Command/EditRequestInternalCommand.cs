using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Solicitacao;

namespace ContractManagement.Application.RequestInternal.Command
{
    public sealed record EditRequestInternalCommand(Guid Id, string Titulo, string Descricao, Periodo? Periodo, string? Comentario) : ICommand
    {
    }
}
