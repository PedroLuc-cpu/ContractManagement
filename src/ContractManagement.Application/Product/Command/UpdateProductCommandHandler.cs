using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Product.Command
{
    internal sealed class UpdateProductCommandHandler(IProdutoRepository produtoRepository) : ICommandHandler<UpdateProductCommand>
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.GetByCodigoAsync(request.Cod, cancellationToken);
            if (produto is null)
            {
                return Result.Failure(new Error("ProductNotFound", "Produto não foi encontrado pelo código informado"));
            }

            produto.AtualizarProduto(request.Name, request.UndMed, request.CodBarr, request.Description);
            await _produtoRepository.UpdateProduto(produto, cancellationToken);

            return Result.Success(produto);

        }
    }
}
