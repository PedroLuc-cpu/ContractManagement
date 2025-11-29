using ContractManagement.Application.Abstractions.Messaging;

namespace ContractManagement.Application.Product.Command
{
    public sealed record CreateProductCommand(
        string ProductName,
        byte[]? Imagem,
        string Description,
        string UndMed,
        string CodBarr,
        string Cod,
        decimal SalePrice,
        decimal BuyPrice,
        int CurrentStock,
        int MinimalStock,
        int MaxStock,
        bool Actve
    ) : ICommand
    {

    }
}
