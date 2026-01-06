namespace ContractManagement.Domain.Interfaces.Gateway
{
    public interface IPagamentoGateway
    {
        Task<string> CriarCheckoutAsync(Guid IdPedido, decimal Valor);

    }
}
