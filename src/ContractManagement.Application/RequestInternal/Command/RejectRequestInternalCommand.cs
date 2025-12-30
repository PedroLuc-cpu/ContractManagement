using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.RequestInternal.Command
{
    public sealed record RejectRequestInternalCommand(Guid Id, string? Comentario) : ICommand
    {
    }
}
