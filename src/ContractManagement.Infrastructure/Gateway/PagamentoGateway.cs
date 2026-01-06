using ContractManagement.Domain.Interfaces.Gateway;

namespace ContractManagement.Infrastructure.Gateway
{
    public class PagamentoGateway : IPagamentoGateway
    {
        public Task<string> CriarCheckoutAsync(Guid IdPedido, decimal Valor)
        {
            throw new NotImplementedException();
        }
    }
}
