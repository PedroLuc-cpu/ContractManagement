namespace ContractManagement.Application.Order.Command
{
    public sealed record CreateOrderItemCommand(Guid ProductId, string NomeProduto, int Quantity, decimal UnitPrice)
    {
    }
}
