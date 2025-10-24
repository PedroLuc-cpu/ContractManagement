namespace ContractManagement.Domain.DTO
{
    public sealed record ProdutoRequestDto(
        string Nome,
        string UnidadeMedida,
        string CodigoBarras,
        string Observacao,
        string Codigo,
        decimal PrecoVenda,
        decimal PrecoCusto,
        int EstoqueAtual,
        int EstoqueMinino,
        int EstoqueMaximo,
        bool Ativo
    )
    {
    }
}
