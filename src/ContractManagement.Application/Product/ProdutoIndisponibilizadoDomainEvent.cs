
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Application.Product
{
    public record ProdutoIndisponibilizadoDomainEvent(Guid ProdutoId, string ProdutoName) : IDomainEvent
    {
    }
}
