using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.ValueObjects;
using ContractManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManagement.Persistence.Repository.Catalogo
{
    public class ProdutoRepository(ContractManagementContext context) : BaseRepository<Produto>(context), IProdutoRepository
    {
        public async Task AdicionarPromocaoAsync(Guid produtoId, decimal desconto, DateTime inicio, DateTime fim, CancellationToken cancellationToken = default)
        {
            var produto = await _dbSet.Where(p => p.Id == produtoId).FirstOrDefaultAsync(cancellationToken);
            var promocao = new Promocao(desconto, new PeriodoPromocional(inicio, fim));
            produto?.AdicionarPromocao(promocao);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateProduto(Produto produto, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(produto, cancellationToken);

        }

        public async Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            var produto = await _dbSet.Where(p => p.Codigo == codigo)
                .FirstOrDefaultAsync(cancellationToken);
            return produto;
        }

        public async Task UpdateProduto(Produto produto, CancellationToken cancellationToken = default)
        {
            await _dbSet.Where(p => p.Codigo == produto.Codigo).ExecuteUpdateAsync(set => set
                .SetProperty(p => p.Nome, produto.Nome)
                .SetProperty(p => p.UnidadeMedida, produto.UnidadeMedida)
                .SetProperty(p => p.CodigoBarras, produto.CodigoBarras)
                .SetProperty(p => p.Observacao, produto.Observacao)
                .SetProperty(p => p.Ativo, produto.Ativo)
                .SetProperty(p => p.DataAtualizao, produto.DataAtualizao)
                ,cancellationToken);
            _context.SaveChanges();
        }
    }
}
