using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Order.Command
{
    public sealed record CreateOrderCommand(Guid IdCliente, IReadOnlyCollection<CreateOrderItemInput> Itens) : ICommand
    {

    }
}
