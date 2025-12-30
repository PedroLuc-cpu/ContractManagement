using ContractManagement.Domain.Erros;
using ContractManagement.Domain.Shared;

namespace ContractManagement.Domain.Entity.Solicitacao
{
    public sealed class Periodo
    {
        public DateTime Inicio { get; private set; }
        public DateTime Fim {  get; private set; }
        public Periodo(DateTime inicio, DateTime fim)
        {
            if (fim > inicio)
            {
                Result.Failure(DomainErrors.Period.NegativeValue);
            }
            Inicio = inicio;
            Fim = fim;
        }
    }
}
