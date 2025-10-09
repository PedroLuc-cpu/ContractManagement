using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Application.Contracts.Services
{
    public interface IPedidoService
    {
        Task AdicionarValor(Guid idPedido, decimal Valor);
        Task<PedidoEntity> CriarPedido();
    }
}
