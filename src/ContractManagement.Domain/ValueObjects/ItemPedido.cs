using ContractManagement.Domain.Common.Base;
using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;

namespace ContractManagement.Domain.ValueObjects
{
    public class ItemPedido : ValueObject
    {
        public Guid IdProduto { get; private set; }
        public string Produto { get; private set; } = string.Empty;
        public int Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        private ItemPedido() { }

        public ItemPedido(Guid produtoId, string nomeProduto, int quantidade, decimal precoUnitario)
        {
            Guard.AgainstEmptyGuid(produtoId, nameof(produtoId));
            Guard.AgaintNull(nomeProduto, nameof(nomeProduto));
            Guard.Againts<DomainException>(quantidade <= 0, "Quantidade deve ser maior que zero.");
            Guard.Againts<DomainException>(precoUnitario <= 0, "Preço unitário deve ser maior que zero.");

            IdProduto = produtoId;
            Produto = nomeProduto;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public decimal SubTotal => Quantidade * PrecoUnitario;
        public void AtualizarQuantidade(int novaQuantidade)
        {
            Guard.Againts<DomainException>(novaQuantidade <= 0, "Quantidade inválida");
            Quantidade = novaQuantidade;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IdProduto;
            yield return Produto;
            yield return Quantidade;
            yield return PrecoUnitario;
        }
    }
}
