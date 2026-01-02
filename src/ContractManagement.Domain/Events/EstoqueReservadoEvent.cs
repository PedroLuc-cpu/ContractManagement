using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Events
{
    public class EstoqueReservadoEvent(Guid idProduto, int quantidadeReservada) : IDomainEvent
    {
        public Guid IdProduto { get; } = idProduto;
        public int QuantidadeReservada { get; } = quantidadeReservada;
    }
}
