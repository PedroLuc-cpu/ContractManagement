namespace ContractManagement.Application.Query.GetItemOrderById
{
    public sealed record ItemOrderResponse(Guid ItemOrder, string ProductName, int Quantity, decimal UnitPrice);
}
