using ContractManagement.Application.Contracts.Repository.IPedido;
using ContractManagement.Application.Contracts.Services;
using ContractManagement.Application.Interfaces;
using ContractManagement.Domain.Entity.Pedido;
    
namespace ContractManagement.Application.Services
{
    public class PedidoService(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork) : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task AdicionarValor(Guid idPedido, decimal Valor)
        {
            var pedido = await _pedidoRepository.GetByIdAsync(idPedido) ?? throw new Exception("Pedido não foi encontrado");

            pedido.AdicionarItem(Valor);

            await _pedidoRepository.UpdateAsync(pedido);
            await _unitOfWork.Commit();
        }

        public async Task<PedidoEntity> CriarPedido()
        {
            var pedido = new PedidoEntity(Guid.NewGuid(), 1, "1234");
            await _pedidoRepository.InsertAsync(pedido);
            await _unitOfWork.Commit();
            return pedido;
        }
    }
}
