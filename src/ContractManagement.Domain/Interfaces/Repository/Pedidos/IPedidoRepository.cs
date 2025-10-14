using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository.Pedidos
{
    public interface IPedidoRepository : IBaseRepository<PedidoEntity>
    {
        Task<PedidoEntity> ListarComItens(Guid id);
    }
}
