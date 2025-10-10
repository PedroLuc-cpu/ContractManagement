using ContractManagement.Application.Contracts.Services;
using ContractManagement.Application.Interfaces;
using ContractManagement.Application.Interfaces.Repository.IPedido;
using ContractManagement.Domain.Entity.Pedido;

namespace ContractManagement.Application.Services
{
    public class PedidoService(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork, IPedidoItemRepository pedidoItemRepository) : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IPedidoItemRepository _pedidoItemRepository = pedidoItemRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task AdicionarItemPedido(Guid pedidoId, Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            var item = new ItemPedidoEntity(produtoId, nomeProduto, quantidade, precoUnitario);          
                await _pedidoItemRepository.AdicionarItemPedidoAsync(item);
                await _unitOfWork.Commit();            
            
        }

        public async Task AdicionarValor(Guid idPedido, decimal Valor)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(idPedido) ?? throw new Exception("Pedido não foi encontrado");

            pedido.AdicionarItem(Valor);

            await _pedidoRepository.UpdateAsync(pedido);
            await _unitOfWork.Commit();
        }

        public async Task<PedidoEntity> CriarPedido(string numero)
        {
            var pedido = new PedidoEntity(Guid.NewGuid(), 1, numero);
            await _pedidoRepository.InsertAsync(pedido);
            await _unitOfWork.Commit();
            return pedido;
        }
    }
}
