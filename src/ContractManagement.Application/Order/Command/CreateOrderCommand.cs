using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Order.Command
{
    public sealed record CreateOrderCommand(Guid IdCliente, List<OrderItemDTO> Itens) : ICommand
    {

    }

    public sealed record OrderItemDTO(Guid ProductId, string NomeProduto, int Quantity, decimal UnitPrice) : ICommand
    {
    }
}
