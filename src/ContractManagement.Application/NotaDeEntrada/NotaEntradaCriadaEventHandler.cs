using ContractManagement.Domain.Entity.Estoques;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using MediatR;

namespace ContractManagement.Application.NotaDeEntrada
{
    internal sealed class NotaEntradaCriadaEventHandler(IEstoqueRepository estoqueRepository, IUnitOfWork unitOfWork) : INotificationHandler<NotaEntradaCriadaEvent>
    {
        private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task Handle(NotaEntradaCriadaEvent notification, CancellationToken cancellationToken)
        {
            foreach (var item in notification.Itens)
            {
                var estoque = await _estoqueRepository.ObterProdutoId(item.IdProduto, cancellationToken);

                if (estoque is null)
                {
                    estoque = Estoque.Create(item.IdProduto, item.Quantidade);
                    await _estoqueRepository.Adicionar(estoque, cancellationToken);
                }
                else
                {
                    estoque.ReporEstoque(item.Quantidade);

                }
            }
            
            await _unitOfWork.Commit(cancellationToken);

        }
    }
}
