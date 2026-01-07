using ContractManagement.Domain.Dto;

namespace ContractManagement.Domain.Interfaces.Payment
{
    public interface IPayments
    {
        Task<string> CriarCheckoutAsync(Guid IdPedido, IReadOnlyCollection<ItemCheckout> itens);

    }
}
