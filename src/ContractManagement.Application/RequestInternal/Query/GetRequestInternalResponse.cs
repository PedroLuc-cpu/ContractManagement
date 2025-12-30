using ContractManagement.Domain.Entity.Solicitacao;
using ContractManagement.Domain.Enums;

namespace ContractManagement.Application.RequestInternal.Query
{
    public sealed record GetRequestInternalResponse(
        Guid Id, Guid IdFuncionario, DateTime DataCriacao, Periodo? Periodo, string Titulo, string Descricao, IEnumerable<SolitacaoHistorico> Historicos, SolicitacaoInternaMetodo Status)
    {
    }
}
