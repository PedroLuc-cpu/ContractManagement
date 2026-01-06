using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Estoques.Command
{
    internal sealed class EstoqueCriadaCommandHandler(IEstoqueRepository estoqueRepository, IUnitOfWork unitOfWork) : ICommandHandler<EstoqueCriadaCommand>
    {
        private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result> Handle(EstoqueCriadaCommand request, CancellationToken cancellationToken)
        {
            var estoqueResult = await _estoqueRepository.ObterProdutoId(request.IdProduto, cancellationToken);

            if (estoqueResult is not null)
            {
                return Result.Failure(new Error("Estoque", "Já existe um estoque cadastrado para este produto."));
            }

            var estoque = Estoque.Create(
                request.IdProduto,
                request.QuantidadeInicial
            );
            
            await _estoqueRepository.Adicionar(estoque, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
