using ContractManagement.Domain.Entity.Estoques;

namespace ContractManagement.Domain.Interfaces.Repository
{
    public interface IEstoqueRepository
    {
        Task<Estoque?>  ObterProdutoId(Guid idProduto, CancellationToken cancellationToken);
        Task Adicionar(Estoque estoque, CancellationToken cancellationToken);
    }
}
