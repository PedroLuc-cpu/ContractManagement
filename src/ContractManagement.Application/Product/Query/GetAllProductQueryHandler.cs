using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Product.Query
{
    internal sealed class GetAllProductQueryHandler(IRedisCacheRepository redisCacheRepository, IProdutoRepository produtoRepository) : IQueryHandler<GetAllProductQuery, IEnumerable<GetProductResponse>>
    {
        private readonly IRedisCacheRepository _redisCacheRepository = redisCacheRepository; 
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        public async Task<Result<IEnumerable<GetProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {           
            const string key = nameof(Product);
            var cacheProducts = _redisCacheRepository.GetData<IEnumerable<GetProductResponse>>(key);
            
            if (cacheProducts is not null)
            {
                return Result.Success(cacheProducts);
            }
            var products = await _produtoRepository.GetAllAsync(cancellationToken);

            var response = products.Select(p => new GetProductResponse(
                p.Id,
                p.Nome,
                p.Imagem,
                p.Ativo,
                p.Observacao,
                p.CodigoBarras,
                p.Codigo,
                p.UnidadeMedida,
                p.PrecoVenda.Value, p.PrecoCusto.Value,
                p.EstoqueAtual,
                p.EstoqueMinimo,
                p.EstoqueMaximo));

            _redisCacheRepository.SetData(key, response);

            return Result.Success<IEnumerable<GetProductResponse>>(response);
        }
    }
}
