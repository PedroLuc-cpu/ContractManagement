using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Product.Command
{
    public sealed record UpdateProductCommand(string Name, string Cod, string Description, string UndMed, string CodBarr) : ICommand
    {
    }
}
