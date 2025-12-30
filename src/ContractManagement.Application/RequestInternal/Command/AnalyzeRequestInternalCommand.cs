using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.RequestInternal.Command
{
    public sealed record AnalyzeRequestInternalCommand(Guid Id, string? Comentario): ICommand
    {
    }
}
