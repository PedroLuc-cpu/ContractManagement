using ContractManagement.Application.Interfaces;
using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Application.Contracts.Repository.IPedido
{
    public interface IPedidoRepository : IBaseRepository<PedidoEntity>
    {
        Task<PedidoEntity> ListarComItens(Guid id);
    }
}
