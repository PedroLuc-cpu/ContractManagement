namespace ContractManagement.Application.Order.Query
{
    public sealed record GetOrdersQueryResponse(Guid Id, Guid ClientId, string Status, DateTime DateOrder, decimal Total, IEnumerable<GetOrdersQueryResponseItemOrder> Itens)
    {
    }
    public sealed record GetOrdersQueryResponseItemOrder(Guid Id, string ProductName, int Quantity, decimal UnitPrice, decimal SubTotal);
}
