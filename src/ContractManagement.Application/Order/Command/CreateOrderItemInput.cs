namespace ContractManagement.Application.Order.Command
{
    public sealed record CreateOrderItemInput(Guid IdProduto, string NomeProduto, int Quantidade, decimal PrecoUnitario)
    {
    }
}
