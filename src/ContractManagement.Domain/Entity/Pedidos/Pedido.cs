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

        public Pedido(Guid idCliente)
        {
            Guard.AgainstEmptyGuid(idCliente, nameof(idCliente));
            IdCliente = idCliente;
            Numero = Guid.NewGuid().ToString().Replace("-", "")[..8].ToUpper();
        }

        public void AdicionarItem(Guid idProduto, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            Guard.AgainstEmptyGuid(idProduto, nameof(idProduto));
            Guard.Againts<DomainException>(quantidade <= 0, "Quantidade deve ser maior que zero.");
            Guard.Againts<DomainException>(precoUnitario <= 0, "Preço unitário deve ser maior que zero.");

            var itemExistente = _Items.FirstOrDefault(i => i.IdProduto == idProduto);

            if (itemExistente != null)
            {
                itemExistente.AtualizarQuantidade(itemExistente.Quantidade + quantidade);
            }
            else
            {
                var novoItem = new ItemPedido(idProduto, nomeProduto, quantidade, precoUnitario);
                _Items.Add(novoItem);
            }
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
