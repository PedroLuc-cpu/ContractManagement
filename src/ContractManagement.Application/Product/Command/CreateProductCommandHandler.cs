using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;


namespace ContractManagement.Application.Product.Command
{
    internal sealed class CreateProductCommandHandler(IProdutoRepository produtoRepository) : ICommandHandler<CreateProductCommand>
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var produtoExist = await _produtoRepository.GetByCodigoAsync(request.Cod, cancellationToken);

            if (produtoExist is not null)
            {
                return Result.Failure(new Error("ProductCreate", "Já existe um produto cadastrado com este código informado"));
            }
            var produto = Produto.Create(
                request.ProductName,
                request.UndMed,
                request.CodBarr,
                request.Description,
                request.Cod,
                request.SalePrice,
                request.BuyPrice,
                request.CurrentStock,
                request.MinimalStock,
                request.MaxStock,
                request.Actve);

            await _produtoRepository.CreateProduto(produto, cancellationToken);
            return Result.Success();
            
        }
    }
}
