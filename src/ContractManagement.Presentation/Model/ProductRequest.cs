namespace ContractManagement.Presentation.Model
{
    public sealed record ProductRequest(
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
        bool Ativo,
        PromocaoResquet? Promocao
    )
    {
    }

    public sealed record ProdutoUpdateRequest(string Name, string UndMed, string Cod, string CodBarr, string Description, bool Active) { }

    public sealed record PromocaoResquet(decimal DescontoPercentual, DateTime Inicio, DateTime Fim);
}
