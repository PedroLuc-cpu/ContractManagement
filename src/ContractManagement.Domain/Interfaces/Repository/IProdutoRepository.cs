using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
        Task CreateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default);
    }
}
