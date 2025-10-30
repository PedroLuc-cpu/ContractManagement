using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default);
        Task CreateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default);
        Task AdicionarPromocaoAsync(Guid produtoId, decimal desconto, DateTime inicio, DateTime fim, CancellationToken cancellationToken = default);
        Task UpdateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default);


    }
}
