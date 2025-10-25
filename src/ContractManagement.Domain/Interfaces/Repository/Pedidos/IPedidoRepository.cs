using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository.Pedidos
{
    public interface IPedidoRepository : IBaseRepository<Pedido>, IUnitOfWork
    {
        Task<Pedido> ListarComItens(Guid id);
        Task Adicionar(Pedido pedido, CancellationToken cancellationToken = default);
        Task<IEnumerable<Pedido>> ListaPaginada(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    }
}
