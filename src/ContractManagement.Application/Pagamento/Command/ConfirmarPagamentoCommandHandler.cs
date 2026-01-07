using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Pagamento.Command
{
    internal sealed class ConfirmarPagamentoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork) : ICommandHandler<ConfirmarPagamentoCommand>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result> Handle(ConfirmarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(request.IdPedido, cancellationToken);
            if (pedido is null)
            {
                return Result.Failure(new Error("PedidoNaoEncontrado", "O Pedido não foi encontrado"));
            }

            pedido.ConfirmarPagamento();

            await _unitOfWork.Commit(cancellationToken);

            return Result.Success();
        }
    }
}
