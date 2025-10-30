using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Application.Product.Command
{
    internal sealed class CreatePromotionCommandHandler(IProdutoRepository produtoRepository) : ICommandHandler<CreatePromotionCommand>
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<Result> Handle(CreatePromotionCommand request, CancellationToken cancellationToken)
        {
            var produtoExists = await  _produtoRepository.GetByIdAsync(request.ProdutoId, cancellationToken);

            if (produtoExists is null)
            {
                return Result.Failure(new Error("ProdutoNaoEncontrado", "Produto não encontrado pelo id informado"));
            }
            var periodo = new PeriodoPromocional(request.Periodo.Inicio, request.Periodo.Fim);
            var promocao = new Promocao(request.DescontoPercentual, periodo);
            await _produtoRepository.AdicionarPromocaoAsync(produtoExists.Id, request.DescontoPercentual, request.Periodo.Inicio, request.Periodo.Fim, cancellationToken);

            return Result.Success(promocao);
            
        }
    }
}
