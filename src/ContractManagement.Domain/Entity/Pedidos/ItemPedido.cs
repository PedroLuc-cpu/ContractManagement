using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.ValueObjects;

namespace ContractManagement.Domain.Entity.Pedidos
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid IdProduto { get; private set; }
        public string NomeProduto { get; private set; } = string.Empty;
        public Money PrecoUnitario { get; private set; } = Money.Create(0).Value;
        public int Quantidade { get; private set; }
        public Money Total => Money.Create(PrecoUnitario.Value * Quantidade).Value;

        protected ItemPedido() { }


        internal ItemPedido(Guid produtoId, string nomeProduto, int quantidade, Money precoUnitario)
        {
            Guard.AgainstEmptyGuid(produtoId, nameof(produtoId));
            Guard.AgaintNull(nomeProduto, nameof(nomeProduto));
            Guard.Againts<DomainException>(quantidade <= 0, "Quantidade deve ser maior que zero.");
            Guard.Againts<DomainException>(precoUnitario.Value <= 0, "Preço unitário deve ser maior que zero.");

            Id = Guid.NewGuid();
            IdProduto = produtoId;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        internal void AtualizarQuantidade(int novaQuantidade)
        {
            Guard.Againts<DomainException>(novaQuantidade <= 0, "Quantidade deve ser maior que zero.");
            Quantidade += novaQuantidade;
        }
    }
}
