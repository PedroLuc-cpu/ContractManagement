using ContractManagement.Domain.Entity.Pedidos;

namespace ContractManagement.Domain.Test.Pedidos
{
    public class PedidoTests
    {
        [Fact]
        public void CriarPedidoDeveRetornarPedidoComStatusPendente()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var pedido = new Pedido(clienteId);
            // Act
            var status = pedido.Status;
            // Assert
            Assert.Equal(Enums.StatusPedidoEnum.Pendente, status);
        }
        [Fact]
        public void AprovarPedidoDeveAlterarStatusParaAprovado()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var pedido = new Pedido(clienteId);
            // Act
            pedido.AprovarPedido();
            var status = pedido.Status;
            // Assert
            Assert.Equal(Enums.StatusPedidoEnum.Aprovado, status);
        }
        [Fact]
        public void AdicionarItemDeveAdicionarItemAoPedido()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var pedido = new Pedido(clienteId);
            var produtoId = Guid.NewGuid();
            var nomeProduto = "Produto Teste";
            var quantidade = 2;
            var precoUnitario = 50m;
            // Act
            pedido.AdicionarItem(produtoId, nomeProduto, quantidade, precoUnitario);
            var itens = pedido.Items;
            // Assert
            Assert.Single(itens);
            Assert.Equal(produtoId, itens.First().IdProduto);
            Assert.Equal(quantidade, itens.First().Quantidade);
            Assert.Equal(precoUnitario, itens.First().PrecoUnitario);
        }
    }        
}

