namespace ContractManagement.UI.Blazor.DTOs
{
    public sealed record ProdutoDto(
        Guid Id,
        string Name,
        bool Active,
        string Description,
        string CodBarr,
        string Cod,
        string UnidMed,
        decimal SalePrice,
        decimal BuyPrice,
        int CurrentStock,
        int MinimalStock,
        int MaxStock
    )
    {
    }
}
