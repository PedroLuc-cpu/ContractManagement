using ContractManagement.Domain.Entity.Solicitacao;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.Interfaces.Repository
{
    public interface ISolicitacaoInternaRepository : IBaseRepository<SolicitacaoInterna>
    {
        Task Add(SolicitacaoInterna solicitacaoInterna, CancellationToken cancellationToken = default);
    }
}
