using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Application.Interfaces.Repository.IPedido
{
    public interface IPedidoRepository : IBaseRepository<PedidoEntity>
    {
        Task<PedidoEntity> ListarComItens(Guid id);
    }
}
