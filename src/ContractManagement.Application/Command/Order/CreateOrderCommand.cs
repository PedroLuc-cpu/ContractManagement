using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Command.Pedidos
{
    public sealed record CreateOrderCommand(string Numero) : ICommand
    {

    }
}
