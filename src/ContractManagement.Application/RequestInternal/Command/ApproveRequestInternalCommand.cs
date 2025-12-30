using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.RequestInternal.Command
{
    public sealed record ApproveRequestInternalCommand(Guid Id, string? Comentario) : ICommand
    {
    }
    
}
