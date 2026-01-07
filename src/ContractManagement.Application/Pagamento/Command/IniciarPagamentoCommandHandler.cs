using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Dto;
using ContractManagement.Domain.Interfaces.Gateway;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Pagamento.Command
{
    internal sealed class IniciarPagamentoCommandHandler(IPedidoRepository pedidoRepository, IPagamentoGateway pagamentoGateway) : ICommandHandler<IniciarPagamentoCommand, string>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPagamentoGateway _pagamentoGateway = pagamentoGateway;
        public async Task<Result<string>> Handle(IniciarPagamentoCommand request, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPedidoPorId(request.IdPedido, cancellationToken);
            if (pedido is null)
            {
                return Result.Failure<string>(new Error("OrderNotFound", "O Pedido não foi encontrado."));
            }

            pedido.IniciarPagamento();

            var itens = pedido.Items.Select(i => new ItemCheckout(i.NomeProduto, i.Quantidade, (long)(i.PrecoUnitario.Value * 100))).ToList();

            var checkoutUrl = await _pagamentoGateway.CriarCheckoutAsync(pedido.Id, itens);

            return Result.Success(checkoutUrl);
        }
    }
}
