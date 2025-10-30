using ContractManagement.Domain.DTO;
using ContractManagement.Domain.Entity;
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

        public async Task CreateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default)
        {
            var produto = Produto.Create(
                produtoRequest.Nome,
                produtoRequest.UnidadeMedida,
                produtoRequest.CodigoBarras,
                produtoRequest.Observacao,
                produtoRequest.Codigo,
                Money.Create(produtoRequest.PrecoVenda).Value,
                Money.Create(produtoRequest.PrecoCusto).Value,
                produtoRequest.EstoqueAtual,
                produtoRequest.EstoqueMinino,
                produtoRequest.EstoqueMaximo,
                produtoRequest.Ativo);
            await _dbSet.AddAsync(produto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }

        public async Task<Produto?> GetByCodigoAsync(string codigo, CancellationToken cancellationToken = default)
        {
            var produto = await _dbSet.Where(p => p.Codigo == codigo)
                .FirstOrDefaultAsync(cancellationToken);
            return produto;
        }

        public async Task UpdateProduto(ProdutoRequestDto produtoRequest, CancellationToken cancellationToken = default)
        {
            var produto = await _dbSet.Where(p => p.Codigo == produtoRequest.Codigo || p.CodigoBarras == p.CodigoBarras || p.Nome == produtoRequest.Nome).FirstOrDefaultAsync(cancellationToken);
            produto.AtualizarProduto(produtoRequest.Nome, produtoRequest.UnidadeMedida, produtoRequest.CodigoBarras, produtoRequest.Observacao);

            await _dbSet.ExecuteUpdateAsync(set => set
                .SetProperty(p => p.Nome, produto.Nome)
                .SetProperty(p => p.UnidadeMedida, produto.UnidadeMedida)
                .SetProperty(p => p.CodigoBarras, produto.CodigoBarras)
                .SetProperty(p => p.Observacao, produto.Observacao)
                );
            _context.SaveChanges();

        }
    }
}
