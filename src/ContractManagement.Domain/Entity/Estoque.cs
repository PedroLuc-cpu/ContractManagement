using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Events;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity
{
    public class Estoque(Guid id, DateTime dataCriacao) : AggregateRoot(id, dataCriacao)
    {
        public Guid IdProduto { get; private set; }
        public int QuantidadeDisponivel { get; private set; }
        public int QuantidadeReservada { get; private set; }

        protected Estoque() : this(Guid.Empty, DateTime.UtcNow) { }
        private Estoque(Guid idProduto, int quantidadeInicial) : this(Guid.NewGuid(), DateTime.UtcNow)
        {
            IdProduto = idProduto;
            QuantidadeDisponivel = quantidadeInicial;
            QuantidadeReservada = 0;
        }

        public static Estoque Create(Guid idProduto, int quantidadeInicial)
        {
            Guard.AgainstEmptyGuid(idProduto, nameof(idProduto));
            Guard.Againts<ArgumentException>(quantidadeInicial < 0, "Quantidade inicial não pode ser negativa.");

            var estoque = new Estoque(idProduto, quantidadeInicial);
            return estoque;
        }

        public bool PossuiEstoqueDisponivel(int quantidade)
        {
            return QuantidadeDisponivel >= quantidade;
        }

        public void ReservarEstoque(int quantidade)
        {
            Guard.Againts<ArgumentException>(quantidade <= 0, "Quantidade a reservar deve ser maior que zero.");
            Guard.Againts<InvalidOperationException>(quantidade > QuantidadeDisponivel, "Quantidade insuficiente em estoque para reserva.");
            QuantidadeDisponivel -= quantidade;
            QuantidadeReservada += quantidade;

            RaiseDomainEvent(new EstoqueReservadoEvent(IdProduto, quantidade));
        }
    }
}
