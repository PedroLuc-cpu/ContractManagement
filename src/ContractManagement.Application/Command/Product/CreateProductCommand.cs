using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Command.Product
{
    public sealed record CreateProductCommand(string ProductName, int Quantity, decimal UnitPrice) : ICommand
    {

    }
}
