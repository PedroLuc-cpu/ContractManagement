using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Enums;
using ContractManagement.Domain.Events;
using ContractManagement.Domain.Primitives;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public class Pedido : AggregateRoot
    {
        public Guid IdCliente { get;  private set; }
        public NumeroPedido Numero { get; private set; }
        public StatusPedidoEnum Status { get; private set; }
        public Money ValorTotal { get; private set; } = Money.Create(0).Value;
        private readonly List<ItemPedido> _Items = [];
        public IReadOnlyCollection<ItemPedido> Items => _Items.AsReadOnly();

        protected Pedido(): base(id: Guid.Empty, dataCriacao: DateTime.UtcNow) { }
        private Pedido(Guid idCliente): base(Guid.NewGuid(), dataCriacao: DateTime.UtcNow) {
            IdCliente = idCliente;
            Numero = NumeroPedido.Create($"PED-{Guid.NewGuid().ToString().Replace("-", "")[..8].ToUpper()}");
            Status = StatusPedidoEnum.Criado;
            ValorTotal = Money.Create(0).Value;
        }

        public static Pedido Create(Guid idCliente, IReadOnlyCollection<PedidoItemData> itens)
        {
            Guard.AgainstEmptyGuid(idCliente, nameof(idCliente));
            Guard.Againts<DomainException>(itens.Count == 0, "Um pedido deve conter ao menos um item.");

            var pedido = new Pedido(idCliente);

            foreach (var item in itens)
            {
                pedido.AdicionarItem(item.IdProduto, item.NomeProduto, item.PrecoUnitario, item.Quantidade);
            }

            pedido.RaiseDomainEvent(
                    new PedidoCriadoEvent(
                        pedido.Id,
                        [.. pedido.Items.Select(i =>
                            new ItemPedidoSnapshot(i.IdProduto, i.Quantidade)
                        )]
                    )
                );


            return pedido;
        }

        public void AdicionarItem(Guid idProduto, string nomeProduto, decimal precoUnitario, int quantidade)
        {           
            ValidarPeditoEditavel();
            Guard.Againts<DomainException>(quantidade <= 0, "Quantidade deve ser maior que zero.");

            var ItemExistente = _Items.FirstOrDefault(i => i.IdProduto == idProduto);

            if (ItemExistente is not null)
            {
                ItemExistente.AtualizarQuantidade(quantidade);
            }
            else
            {
                _Items.Add(new ItemPedido(idProduto, nomeProduto, quantidade, Money.Create(precoUnitario).Value));  
            }
            RecalcularValorTotal();
        }

        public void RemoverItem(Guid idProduto)
        {
            ValidarPeditoEditavel();

            var item = _Items.FirstOrDefault(i => i.IdProduto == idProduto);

            Guard.Againts<DomainException>(item is null, "Item não encontrado no pedido.");

            _Items.Remove(item!);

            RecalcularValorTotal();
        }

        public void IniciarPagamento()
        {
            Guard.Againts<DomainException>(_Items.Count == 0, "Não é possível finalizar um pedido sem itens.");
            Guard.Againts<DomainException>(Status != StatusPedidoEnum.Pendente, "Não pode iniciar o um pagamento que não pendente.");
            Status = StatusPedidoEnum.Pendente;
            RaiseDomainEvent(new PagamentoInciadoEvent(Id, ValorTotal.Value));
        }

        
        public void ConfirmarPagamento()
        {
            Guard.Againts<DomainException>(Status != StatusPedidoEnum.Pendente, "Apenas pedidos pendentes podem ser aprovados.");
            Status = StatusPedidoEnum.Autorizado;
            RaiseDomainEvent(new PagamentoConfirmadoEvent(Id, ValorTotal.Value));


        }
        private void ValidarPeditoEditavel()
        {
            Guard.Againts<DomainException>(Status == StatusPedidoEnum.Autorizado, "Não é possível editar um pedido aprovado.");
            Guard.Againts<DomainException>(Status == StatusPedidoEnum.Recusado, "Não é possível editar um pedido rejeitado.");
        }

        private void RecalcularValorTotal()
        {
            ValorTotal = Money.Create(_Items.Sum(i => i.Total.Value)).Value;
        }

    }
}
