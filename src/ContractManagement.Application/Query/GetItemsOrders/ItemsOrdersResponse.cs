namespace ContractManagement.Application.Query.GetItemsOrders
{
    public sealed record ItemsOrdersResponse(Guid ItemOrder, string ProductName, int Quantity, decimal UnitPrice);
}
