using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Infrastructure.Repository.Contracts.IPedido
{
    public interface IPedidoRepository
    {
        Task Inserir(PedidoEntity pedido);
        Task Salvar();
        Task<PedidoEntity> ObterPorId(Guid id);
        Task Atualizar(PedidoEntity pedido);
        Task<List<PedidoEntity>> Listar();

    }
}
