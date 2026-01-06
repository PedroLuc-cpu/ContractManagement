using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Estoques;

namespace ContractManagement.Application.NotaDeEntrada
{
    public sealed record RegistrarNotaEntradaCommand(string NumeroDocumento, IReadOnlyCollection<ItemNotaEntradaData> Itens) : ICommand
    {
    }
}
