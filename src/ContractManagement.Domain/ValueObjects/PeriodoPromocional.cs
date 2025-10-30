using ContractManagement.Domain.Common.Exceptions;
using ContractManagement.Domain.Common.Validations;
using ContractManagement.Domain.Primitives;

namespace ContractManagement.Domain.ValueObjects
{
    public class PeriodoPromocional : ValueObject
    {
        public DateTime Inicio { get; }
        public DateTime Fim {  get; }
        private PeriodoPromocional() { }

        public PeriodoPromocional(DateTime inicio, DateTime fim)
        {
            Guard.Againts<DomainException>(fim <= inicio, "Data final deve ser posterior à data inicial");

            Inicio = inicio;
            Fim = fim;
        }
        public bool IsActive() => DateTime.UtcNow >= Inicio && DateTime.UtcNow <= Fim;

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return Inicio;
            yield return Fim;
        }
    }
}
