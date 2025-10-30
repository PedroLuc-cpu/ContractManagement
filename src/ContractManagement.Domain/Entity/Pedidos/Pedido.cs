using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public class Pedido : AggregateRoot
    {
        public Guid IdCliente { get;  private set; }
        public string Numero { get; private set; } = string.Empty;
        public OrderStatus Status { get; private set; } = OrderStatus.Pendente;
        public Money ValorTotal { get; private set; }
        private readonly List<ItemPedido> _Items = [];
        public IReadOnlyCollection<ItemPedido> Items => _Items.AsReadOnly();

        protected Pedido(): base(id: Guid.Empty) { }
        private Pedido(Guid idCliente, string numero, OrderStatus status): base(Guid.NewGuid()) {
            IdCliente = idCliente;
            Numero = numero;
            Status = status;
            ValorTotal = Money.Create(0).Value;
        }

        public static Pedido Create(Guid idCliente)
        {
            Guard.AgainstEmptyGuid(idCliente, nameof(idCliente));
            var numero = Guid.NewGuid().ToString().Replace("-", "")[..8].ToUpper();
            
            var pedido = new Pedido(idCliente, numero, OrderStatus.Pendente);
            return pedido;
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
                var novoItem = new ItemPedido(idProduto, nomeProduto, quantidade, Money.Create(precoUnitario).Value);
                _Items.Add(novoItem);
            }
        }
        
        public void FinalizarPedido()
        {
            Guard.Againts<DomainException>(Items.Count == 0, "Não é possível finalizar um pedido sem itens.");
            ValorTotal = Money.Create(CalcularTotal()).Value;
            AlterarStatus(OrderStatus.Concluido);
            Status = OrderStatus.Concluido;

        }

        public void CancelarPedido()
        {
            Guard.Againts<DomainException>(Status == OrderStatus.Cancelado, "Pedido já está cancelado.");
            AlterarStatus(OrderStatus.Cancelado);
            Status = OrderStatus.Cancelado;

        }
        public void AprovarPedido()
        {
            Guard.Againts<DomainException>(Status == OrderStatus.Aprovado, "Pedido já está aprovado.");
            AlterarStatus(OrderStatus.Aprovado);
            Status = OrderStatus.Aprovado;

        }

        private void AlterarStatus(OrderStatus novoStatus)
        {
            if (!Status.CanTransitionTo(novoStatus))
            {
                throw new DomainException($"Transição de status inválida de {Status} para {novoStatus}.");
            }
            Status = novoStatus;
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
