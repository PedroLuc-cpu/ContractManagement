using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Pedido
{
    public class PedidoEntity : EntityBase, IAggregateRoot
    {
        public Guid IdCliente { get;  private set; }
        public string Numero { get; private set; } = string.Empty;
        public decimal ValorTotal { get; private set; }
        private readonly List<ItemPedidoEntity> _Items = [];
        public IReadOnlyCollection<ItemPedidoEntity> Items => _Items.AsReadOnly();

        private PedidoEntity() { }

        public PedidoEntity(Guid idCliente, decimal valorTotal, string numero)
        {
            Guard.AgainstEmptyGuid(idCliente, nameof(idCliente));
            Guard.AgaintNull(valorTotal, nameof(valorTotal));
            Guard.Againts<DomainException>(valorTotal <= 0, "O ValorToal não pode ser zero");
            Guard.AgaintNull(numero, nameof(numero));

            Id = Guid.NewGuid();
            IdCliente = idCliente;
            Numero = numero;
        }
        
        public void AdicionarItem(decimal valor)
        {
            Guard.AgaintNull(valor, nameof(valor));
            Guard.Againts<DomainException>(valor <= 0, "O ValorToal não pode ser zero");
            ValorTotal += valor;
            SetDataAtualizacao();
        }
        public void AdicionarItem(ItemPedidoEntity item)
        {
            Guard.AgaintNull(item, nameof(item));
            _Items.Add(item);
        }

        public void RemoverItem(Guid idProduto)
        {
            Guard.AgainstEmptyGuid(idProduto, nameof(idProduto));
            var item = _Items.FirstOrDefault(i => i.IdProduto == idProduto);
            Guard.Againts<DomainException>(item == null, "Item não encontrado no pedido");
        }

        public decimal CalcularTotal() => _Items.Sum(i => i.SubTotal);
    }
}
