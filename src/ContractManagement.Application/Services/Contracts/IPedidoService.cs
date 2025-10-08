using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Application.Services.Contracts
{
    public interface IPedidoService
    {
        Task AdicionarValor(Guid idPedido, decimal Valor);
        Task<Pedido> CriarPedido();
    }
}
