using ContractManagement.Application.Services.Contracts;
using ContractManagement.Domain.Entity.Pedido;
using ContractManagement.Infrastructure.Repository.Contracts.IPedido;

namespace ContractManagement.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task AdicionarValor(Guid idPedido, decimal Valor)
        {
            var pedido = await _pedidoRepository.ObterPorId(idPedido) ?? throw new Exception("Pedido não foi encontrado");

            pedido.AdicionarItem(Valor);

            await _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.Salvar();
        }

        public async Task<PedidoEntity> CriarPedido()
        {
            var pedido = new PedidoEntity();
            await _pedidoRepository.Inserir(pedido);
            await _pedidoRepository.Salvar();
            return pedido;
        }

    }
}
