using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Catalogo;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Interfaces.Services;
using ContractManagement.Domain.Shared;


namespace ContractManagement.Application.Product.Command
{
    internal sealed class CreateProductCommandHandler(IProdutoRepository produtoRepository, IEmailService emailService) : ICommandHandler<CreateProductCommand>
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IEmailService _emailService = emailService;

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var produtoExist = await _produtoRepository.GetByCodigoAsync(request.Cod, cancellationToken);

            if (produtoExist is not null)
            {
                return Result.Failure(new Error("ProductCreate", "Já existe um produto cadastrado com este código informado"));
            }
            var produto = Produto.Create(
                request.ProductName,
                request.Imagem,
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
            await _emailService.SendEmailAsync("gemaluzente2015@gmail.com", "O Produto foi cadastrado com sucesso", "<h1>Obrigado por se registrar 🎉</h1>");
            return Result.Success();
        }
    }
}
