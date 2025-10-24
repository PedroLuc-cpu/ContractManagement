using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public class Pedido : EntityBase, IAggregateRoot
    {
        public Guid IdCliente { get;  private set; }
        public string Numero { get; private set; } = string.Empty;
        public decimal ValorTotal { get; private set; }
        private readonly List<ItemPedido> _Items = [];
        public IReadOnlyCollection<ItemPedido> Items => _Items.AsReadOnly();

        private Pedido() { }

        public Pedido(Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            var item = new ItemPedido(produtoId, nomeProduto, quantidade, precoUnitario);
            _Items.Add(item);
            CalcularTotal();
            SetDataAtualizacao();

        }

        public void AdicionarItem(ItemPedido item)
        {
            Guard.AgaintNull(item, nameof(item));
            _Items.Add(item);
        }

        public void RemoverItem(Guid idProduto)
        {
            Guard.AgainstEmptyGuid(idProduto, nameof(idProduto));
            var item = _Items.FirstOrDefault(i => i.IdProduto == idProduto);
            Guard.Againts<DomainException>(item is null, "Item não encontrado no pedido");
        }

        public decimal CalcularTotal() => _Items.Sum(i => i.SubTotal);
    }
}
