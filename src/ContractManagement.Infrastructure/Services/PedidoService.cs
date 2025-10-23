using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Interfaces;
using ContractManagement.Domain.Interfaces.Repository.Pedidos;
using ContractManagement.Domain.Interfaces.Services;


namespace ContractManagement.Infrastructure.Services
{
    public class PedidoService(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork, IPedidoItemRepository pedidoItemRepository) : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository = pedidoItemRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task AdicionarItemPedido(Guid pedidoId, Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            var item = new ItemPedido(produtoId, nomeProduto, quantidade, precoUnitario);          
                await _pedidoItemRepository.AdicionarItemPedidoAsync(item);
                await _unitOfWork.SaveChangesAsync();            
            
        }

        public async Task AdicionarValor(Guid idPedido, decimal Valor)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(idPedido) ?? throw new Exception("Pedido não foi encontrado");

            pedido.AdicionarItem(Valor);

            await _pedidoRepository.UpdateAsync(pedido);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Pedido> CriarPedido(string numero)
        {
            var pedido = new Pedido(Guid.NewGuid(), 1, numero);
            await _unitOfWork.SaveChangesAsync();
            return pedido;
        }
    }
}
