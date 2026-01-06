using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Entity.Estoques
{
    public class EstoqueRepostoEvent(Guid idProduto, int quantidadeReposta) : IDomainEvent
    {
        public Guid IdProduto { get; } = idProduto;
        public int QuantidadeReposta { get; } = quantidadeReposta;
    }
}