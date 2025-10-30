using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Order.Command
{
    internal sealed class CreateOrderCommandHandler(IPedidoRepository pedidoRepository, IClienteRepository clienteRepository) : ICommandHandler<CreateOrderCommand>
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var clienteExists = await _clienteRepository.GetByIdAsync(request.IdCliente, cancellationToken);
            if (clienteExists is null)
            {
                return Result.Failure(new Error("ClienteNaoEncontrado", "Cliente não encontrado para o Id informado."));
            }

            var pedido = Pedido.Create(request.IdCliente);

            foreach (var item in request.Itens)
            {
                pedido.AdicionarItem(item.ProductId, item.NomeProduto, item.Quantity, item.UnitPrice);
            }
            await _pedidoRepository.Adicionar(pedido, cancellationToken);
            await _pedidoRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
