namespace ContractManagement.Domain.Entity.Pedidos
{
    public sealed record PedidoItemData(Guid IdProduto, string NomeProduto, int Quantidade, decimal PrecoUnitario)
    {
    }
}
