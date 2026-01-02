using ContractManagement.Domain.Entity.Pedidos;
using ContractManagement.Domain.Enums;

namespace ContractManagement.Domain.Test.Pedidos
{
    public class PedidoTests
    {
        [Fact]
        public void CriarPedidoDeveRetornarPedidoComStatusPendente()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var pedido = Pedido.Create(clienteId);
            // Act
            var status = pedido.Status;
            // Assert
            Assert.Equal(StatusPedidoEnum.Pendente, status);
        }
        [Fact]
        public void AprovarPedidoDeveAlterarStatusParaAprovado()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var pedido = Pedido.Create(clienteId);
            // Act
            pedido.AprovarPedido();
            var status = pedido.Status;
            // Assert
            Assert.Equal(StatusPedidoEnum.Aprovado, status);
        }
    }        
}

