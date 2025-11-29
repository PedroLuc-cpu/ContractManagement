namespace ContractManagement.Application.Product.Query
{
    public sealed record GetProductResponse(
        Guid Id,
        string Name,
        byte[]? Imagem,
        bool Active,
        string Description,
        string CodBarr,
        string Cod,
        string UnidMed,
        decimal SalePrice,
        decimal BuyPrice,
        int CurrentStock,
        int MinimalStock,
        int MaxStock)
    {
    }
}
