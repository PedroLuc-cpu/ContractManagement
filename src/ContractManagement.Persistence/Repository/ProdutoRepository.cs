using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Infrastructure.Persistence;

namespace ContractManagement.Persistence.Repository
{
    public class ProdutoRepository(ContractManagementContext context) : BaseRepository<Produto>(context), IProdutoRepository
    {
        public async Task CreateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default)
        {
            var produto = new Produto(
                produtoRequest.Nome,
                produtoRequest.UnidadeMedida,
                produtoRequest.CodigoBarras,
                produtoRequest.Observacao,
                produtoRequest.Codigo,
                produtoRequest.PrecoVenda,
                produtoRequest.PrecoCusto,
                produtoRequest.EstoqueAtual,
                produtoRequest.EstoqueMinino,
                produtoRequest.EstoqueMaximo,
                produtoRequest.Ativo);
            await _dbSet.AddAsync(produto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
