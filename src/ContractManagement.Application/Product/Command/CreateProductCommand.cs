using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Product.Command
{
    public sealed record CreateProductCommand(string ProductName, int Quantity, decimal UnitPrice) : ICommand
    {

    }
}
