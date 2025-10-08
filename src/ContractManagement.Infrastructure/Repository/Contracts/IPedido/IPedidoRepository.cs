using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Infrastructure.Repository.Contracts.IPedido
{
    public interface IPedidoRepository
    {
        Task Inserir(Pedido pedido);
        Task Salvar();
        Task<Pedido> ObterPorId(Guid id);
        Task Atualizar(Pedido pedido);
        Task<List<Pedido>> Listar();

    }
}
