using ContractManagement.Application.Abstractions.Messaging;
using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository;
using ContractManagement.Domain.Interfaces.Repository.Clientes;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Interfaces.Services;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Application.Order.Command
{
    internal sealed class CreateOrderCommandHandler(
        IEstoqueRepository estoqueRepository,
        IPedidoRepository pedidoRepository,
        IClienteRepository clienteRepository,
        IEmailService emailService,
        IUnitOfWork unitOfWork
        ) : ICommandHandler<CreateOrderCommand>
    {
        private readonly IEstoqueRepository _estoqueRepository = estoqueRepository;
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IClienteRepository _clienteRepository = clienteRepository;
        private readonly IEmailService _emailService = emailService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var clienteExists = await _clienteRepository.GetByIdAsync(request.IdCliente, cancellationToken);
            if (clienteExists is null)
            {
                return Result.Failure(new Error("ClienteNaoEncontrado", "Cliente não encontrado para o Id informado."));
            }

            foreach (var item in request.Itens)
            {
                var produtoEmEstoque = await _estoqueRepository.ObterProdutoId(item.IdProduto, cancellationToken);

                if (produtoEmEstoque is null)
                {
                    return Result.Failure(new Error("ProdutoNaoEncontrado", $"Produto com Id {item.IdProduto} não encontrado em estoque."));
                }
                
                if (!produtoEmEstoque.PossuiEstoqueDisponivel(item.Quantidade))
                {
                    return Result.Failure(new Error("EstoqueInsufiente", $"Estoque insuficiente para o produto {item.NomeProduto}."));

                }

                produtoEmEstoque.ReservarEstoque(item.Quantidade);
            }

            var itensPedido = request.Itens.Select(item => new PedidoItemData(item.IdProduto, item.NomeProduto, item.Quantidade, item.PrecoUnitario)).ToList();



            var pedido = Pedido.Create(request.IdCliente, itensPedido);
         

            await _pedidoRepository.Adicionar(pedido, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            //await _emailService.SendEmailAsync(clienteExists.Email.Value, "Pedido criado com sucesso", "");


            return Result.Success(pedido.Numero);
        }
    }
}
